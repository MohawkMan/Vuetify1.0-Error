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

        public async Task<TournamentDTO> Register(TournamentRegistrationDTO dto, bool sendEmailConfirmation)
        {
            var registration = await DoRegistration(dto, sendEmailConfirmation);
            return await GetTournamentAsync(registration.TournamentId);
        }
        public async Task<TournamentDTO> BulkRegister(List<TournamentRegistrationDTO> dto)
        {
            //throw new Exception("This is a error");
            TournamentRegistration registration = null;
            foreach(var regDto in dto)
            {
                registration = await DoRegistration(regDto, false);
            }
            return await GetTournamentAsync(registration.TournamentId);
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

            if(tourney.StatusId == 100) //COMPLETE
            {
                tourney.Divisions.RemoveAll(r => !r.Teams.Any());
                foreach(var d in tourney.Divisions)
                {
                    d.RegistrationWindows.Clear();
                }
            }

            return tourney;
        }

        //private async Task<Tournament> CalculateTournamentAsync (Tournament tourney)
        //{
        //    foreach(var division in tourney.Divisions)
        //    {
        //        foreach(var window in division.RegistrationWindows)
        //        {

        //        }
        //    }
        //}
        public async Task<TournamentDTO> CreateTournamentAsync(TournamentDTO dto)
        {
            var tourney = _mapper.Map<Tournament>(dto);
            _db.Tournaments.Add(tourney);
            await _db.SaveChangesAsync();

            foreach(var division in tourney.Divisions)
            {
                if (!_db.Entry(division).Reference(r => r.AgeType).IsLoaded)
                    await _db.Entry(division).Reference(r => r.AgeType).LoadAsync();

                if (!_db.Entry(division).Reference(r => r.Gender).IsLoaded)
                    await _db.Entry(division).Reference(r => r.Gender).LoadAsync();

                if (!_db.Entry(division).Reference(r => r.Division).IsLoaded)
                    await _db.Entry(division).Reference(r => r.Division).LoadAsync();

                if (!_db.Entry(division).Reference(r => r.Location).IsLoaded)
                    await _db.Entry(division).Reference(r => r.Location).LoadAsync();
            }


                return _mapper.Map<TournamentDTO>(tourney);
        }
    }
}
