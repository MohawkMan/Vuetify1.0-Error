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
            return await _db.AgeTypes.ToListAsync();
        }

        public async Task<List<Gender>> GetAllGendersAsync()
        {
            return await _db.Genders.ToListAsync();
        }

        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            return await _db.Divisions.ToListAsync();
        }

        public async Task<List<LocationDTO>> GetAllLocationsAsync()
        {
            return await _db.Locations
                .ProjectTo<LocationDTO>()
                .ToListAsync();
        }

        public async Task<List<LocationDTO>> GetOrganizationLocationsAsync(int? organizationId)
        {
            if(organizationId.HasValue)
                return await _db.OrganizationLocations
                    .Where(w => w.OrganizationId == organizationId)
                    .ProjectTo<LocationDTO>()
                    .ToListAsync();

            return await GetAllLocationsAsync();
        }
    }
}
