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

        public async Task<TournamentDTO> Register(TournamentRegistrationDTO dto)
        {
            var registration = _mapper.Map<TournamentRegistration>(dto);
            _db.TournamentRegistrations.Add(registration);
            await _db.SaveChangesAsync();
            var team = _mapper.Map<TournamentTeam>(registration);
            _db.TournamentTeams.Add(team);
            await _db.SaveChangesAsync();
            var division = await _db.TournamentDivisions.FindAsync(registration.TournamentDivisionId);
            //send emails

            return await GetTournamentAsync(division.TournamentId);
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
                .Where(w=>w.Organization.UserName == organizationUsername)
                .ProjectTo<TournamentDTO>();

            if (publicOnly)
                query = query.Where(w => w.IsPublic);

            return await query.ToListAsync();
        }
        public async Task<TournamentDTO> GetTournamentAsync(int id)
        {
            return await _db.Tournaments
                .Where(w => w.Id == id)
                .ProjectTo<TournamentDTO>()
                .FirstOrDefaultAsync();
        }
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
