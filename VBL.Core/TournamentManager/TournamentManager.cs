using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using VBL.Data.Mapping;
using Hangfire;

namespace VBL.Core
{
    public partial class TournamentManager
    {
        private readonly IMapper _mapper;
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;

        public TournamentManager(IMapper mapper, VBLDbContext db, ILogger<TournamentManager> logger)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
        }

        public async Task<TournamentRegistration> Register(TournamentRegistrationDTO dto, bool sendEmailConfirmation)
        {
            return await DoRegistration(dto, sendEmailConfirmation);
        }
        public async Task<TournamentDTO> BulkRegister(List<TournamentRegistrationDTO> dto, bool overwrite = false)
        {
            //throw new Exception("This is a error");
            if(overwrite)
            {
                var oldRegistrations = await _db.TournamentRegistrations
                    .Include(i => i.TournamentTeam)
                    .Where(w => w.TournamentId == dto[0].TournamentId)
                    .ToListAsync();

                foreach(var r in oldRegistrations)
                {
                    r.IsDeleted = true;
                    var joiner = string.IsNullOrWhiteSpace(r.Notes) ? "": ";";
                    r.Notes = $"{r.Notes}{joiner}Bulk register overwrite: {DateTime.Now}";
                    r.TournamentTeam.IsDeleted = true;
                    joiner = string.IsNullOrWhiteSpace(r.TournamentTeam.Notes) ? "" : ";";
                    r.TournamentTeam.Notes = $"{r.TournamentTeam.Notes}{joiner}Bulk register overwrite: {DateTime.Now}";
                }
            }
            TournamentRegistration registration = null;
            foreach(var regDto in dto)
            {
                registration = await DoRegistration(regDto, false);
            }
            return await GetTournamentAsync(registration.TournamentId);
        }

        public async Task<List<TournamentDTO>> GetTournamentListAsync(List<int> organizationIds)
        {
            var query = _db.Tournaments
                 .ProjectTo<TournamentDTO>()
                 .Where(w => w.StatusId != (int)TournamentStatus.Deleted)
                 .Where(w => w.IsPublic || organizationIds.Contains(w.OrganizationId));

            return await query.ToListAsync();
        }
        public async Task<List<TournamentDTO>> GetTournamentListAsync(bool publicOnly = true, int? organizationId = null)
        {
            var query = _db.Tournaments
                 .ProjectTo<TournamentDTO>();

            if (publicOnly)
                query = query.Where(w => w.IsPublic);

            if (organizationId.HasValue)
                query = query.Where(w => w.OrganizationId == organizationId);

            return await query.ToListAsync();
        }
        public async Task<List<TournamentDTO>> GetTournamentListAsync(bool publicOnly, string organizationUsername)
        {
            var query = _db.Tournaments
                .Where(w=>w.Organization.Username == organizationUsername)
                .ProjectTo<TournamentDTO>();

            if (publicOnly)
                query = query.Where(w => w.IsPublic);

            return await query.ToListAsync();
        }
        public async Task<TournamentDTO> GetTournamentAsync(int id)
        {
            var tourney = await _db.Tournaments
                .Where(w => w.Id == id)
                .ProjectTo<TournamentDTO>()
                .FirstOrDefaultAsync();

            if (tourney.StatusId == 100) //COMPLETE
            {
                tourney.Divisions.RemoveAll(r => !r.Teams.Any());
                foreach (var d in tourney.Divisions)
                {
                    d.RegistrationWindows.Clear();
                }
            }

            return tourney;
        }
        public async Task<TournamentDTO> GetRawTournamentAsync(int id)
        {
            var tourney = await _db.Tournaments
                .Where(w => w.Id == id)
                .ProjectTo<TournamentDTO>()
                .FirstOrDefaultAsync();
            return tourney;
        }
        public async Task<TournamentDTO> GetTournamentCopyAsync(int id)
        {
            var tourney = await _db.Tournaments
                .Where(w => w.Id == id)
                .ProjectTo<TournamentDTO>()
                .FirstOrDefaultAsync();

            tourney.Id = 0;
            tourney.ExternalRegistrationUrl = null;
            tourney.IsPublic = false;
            tourney.StatusId = 0;
            //Ensure all do not have Ids
            foreach (var division in tourney.Divisions)
            {
                division.Id = 0;
                foreach (var day in division.Days)
                {
                    day.Id = 0;
                }
                division.RegistrationFields.Id = 0;
                foreach (var window in division.RegistrationWindows)
                {
                    window.Id = 0;
                }
                division.Teams.Clear();
            }

            return tourney;
        }

        public async Task<TournamentDTO> CreateTournamentAsync(TournamentDTOIncoming dto)
        {
            //Ensure all do not have Ids
            foreach(var division in dto.Divisions)
            {
                division.Id = 0;
                foreach(var day in division.Days)
                {
                    day.Id = 0;
                }
                division.RegistrationFields.Id = 0;
                foreach(var window in division.RegistrationWindows)
                {
                    window.Id = 0;
                }
            }

            var tourney = _mapper.Map<Tournament>(dto);
            _db.Tournaments.Add(tourney);
            await _db.SaveChangesAsync();

            return await GetTournamentAsync(tourney.Id);
        }
        public async Task<TournamentDTO> EditTournamentAsync(TournamentDTOIncoming dto)
        {
            var tournament = await _db.Tournaments
                .Include(i => i.Divisions)
                    .ThenInclude(t => t.Days)
                .Include(i => i.Divisions)
                    .ThenInclude(t => t.RegistrationFields)
                .Include(i => i.Divisions)
                    .ThenInclude(t => t.RegistrationWindows)
                .Where(w => w.Id == dto.Id)
                .FirstOrDefaultAsync();
            if (tournament == null)
                throw new Exception($"Could not find tournament with Id: {dto.Id}");

            _mapper.Map(dto, tournament);
            await _db.SaveChangesAsync();
            return await GetTournamentAsync(tournament.Id);
        }

        public async Task<bool> PublishAsync(int id, bool isPublic)
        {
            var tournament = await _db.Tournaments.FindAsync(id);

            tournament.IsPublic = isPublic;
            await _db.SaveChangesAsync();
            return isPublic;
        }
    }
}
