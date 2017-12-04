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

        public async Task<List<OptionDTO>> GetAllAgeTypeOptionsAsync()
        {
            return await _db.AgeTypes
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Name)
                .ProjectTo<OptionDTO>()
                .ToListAsync();
        }

        public async Task<List<OptionDTO>> GetAllGenderOptionsAsync()
        {
            return await _db.Genders
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Name)
                .ProjectTo<OptionDTO>()
                .ToListAsync();
        }

        public async Task<List<OptionDTO>> GetAllDivisionOptionsAsync()
        {
            return await _db.Divisions
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Order)
                .ProjectTo<OptionDTO>()
                .ToListAsync();
        }

        public async Task<List<OptionDTO>> GetAllLocationOptionsAsync()
        {
            return await _db.Locations
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Name)
                .ProjectTo<OptionDTO>()
                .ToListAsync();
        }

        public async Task<List<OptionDTO>> GetOrganizationLocationOptionsAsync(int? organizationId)
        {
            if(organizationId.HasValue)
                return await _db.OrganizationLocations
                    .Where(w => w.OrganizationId == organizationId)
                    .ProjectTo<OptionDTO>()
                    .OrderBy(o => o.Name)
                    .ToListAsync();

            return await GetAllLocationOptionsAsync();
        }
    }
}
