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
    public partial class OrganizationManager
    {
        private readonly IMapper _mapper;
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;

        public OrganizationManager(IMapper mapper, VBLDbContext db, ILogger<TournamentManager> logger)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
        }

        public async Task<OrganizationDTO> GetOrganizationAsync(int id)
        {
            return await _db.Organizations
                .Where(w => w.Id == id)
                .ProjectTo<OrganizationDTO>()
                .FirstOrDefaultAsync();
        }
        public async Task<OrganizationDTO> GetOrganizationAsync(string username)
        {
            return await _db.Organizations
                .Where(w => w.Username == username)
                .ProjectTo<OrganizationDTO>()
                .FirstOrDefaultAsync();
        }
        public async Task<OrganizationDTO> CreateOrganizationAsync(OrganizationDTO dto)
        {
            var newOrg = _mapper.Map<Organization>(dto);
            var username = newOrg.Name.Replace(" ", "");
            //check if it exists if it does use a guid
            newOrg.Username = username;
            _db.Organizations.Add(newOrg);
            //Send notifications
            await _db.SaveChangesAsync();
            return _mapper.Map<OrganizationDTO>(newOrg);
        }

        public async Task<OrganizationMemberDTO> AddMemberAsync(OrganizationMemberDTO dto)
        {
            var member = _mapper.Map<OrganizationMember>(dto);
            _db.OrganizationMembers.Add(member);
            //Send notifications
            await _db.SaveChangesAsync();
            return _mapper.Map<OrganizationMemberDTO>(member);
        }
    }
}
