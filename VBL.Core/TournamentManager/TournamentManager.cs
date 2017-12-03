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

        public async Task<List<AgeType>> GetAllAgeTypesAsync()
        {
            return await _db.AgeTypes
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<List<Gender>> GetAllGendersAsync()
        {
            return await _db.Genders
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            return await _db.Divisions
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Order)
                .ToListAsync();
        }

        public async Task<List<LocationDTO>> GetAllLocationsAsync()
        {
            return await _db.Locations
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Name)
                .ProjectTo<LocationDTO>()
                .ToListAsync();
        }

        public async Task<List<LocationDTO>> GetOrganizationLocationsAsync(int? organizationId)
        {
            if(organizationId.HasValue)
                return await _db.OrganizationLocations
                    .Where(w => w.OrganizationId == organizationId)
                    .ProjectTo<LocationDTO>()
                    .OrderBy(o => o.Name)
                    .ToListAsync();

            return await GetAllLocationsAsync();
        }
    }
}
