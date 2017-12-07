using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBL.Data.Mapping;

namespace VBL.Core
{
    public partial class TournamentManager
    {
        public async Task<List<OptionDTO>> GetAllAgeTypeOptionsAsync()
        {
            return await _db.AgeTypes
                .Where(w => w.IsPublic)
                .OrderBy(o => o.Id)
                .ProjectTo<OptionDTO>()
                .ToListAsync();
        }

        public async Task<List<Option2DTO>> GetAllGenderOptionsAsync()
        {
            return await _db.Genders
                .Where(w => w.IsPublic)
                .OrderBy(o => o.AgeTypeId).ThenBy(t => t.Order)
                .ProjectTo<Option2DTO>()
                .ToListAsync();
        }

        public async Task<List<Option2DTO>> GetAllDivisionOptionsAsync()
        {
            return await _db.Divisions
                .Where(w => w.IsPublic)
                .OrderBy(o => o.AgeTypeId).ThenBy(t => t.Order)
                .ProjectTo<Option2DTO>()
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
            if (organizationId.HasValue)
                return await _db.OrganizationLocations
                    .Where(w => w.OrganizationId == organizationId)
                    .ProjectTo<OptionDTO>()
                    .OrderBy(o => o.Name)
                    .ToListAsync();

            return await GetAllLocationOptionsAsync();
        }
    }
}
