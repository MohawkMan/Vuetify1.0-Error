﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VBL.Data;
using VBL.Data.Mapping;
using AutoMapper.QueryableExtensions;
using AutoMapper.Configuration;

namespace VBL.Core
{
    public partial class ApplicationUserManager
    {
        private readonly IMapper _mapper;
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;
        public readonly UserManager<ApplicationUser> IdentityManager;

        public ApplicationUserManager(IMapper mapper, VBLDbContext db, UserManager<ApplicationUser> userManager, ILogger<ApplicationUserManager> logger)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
            IdentityManager = userManager;
        }

        public async Task<ApplicationUserDTO> GetMe(int userId)
        {
            return await _db.Users
                .Where(w => w.Id == userId)
                .ProjectTo<ApplicationUserDTO>()
                .FirstOrDefaultAsync();
        }
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            var role = await _db.Roles.FirstOrDefaultAsync(f => f.Name == roleName);
            if(role != null)
            {
                var userRole = await _db.UserRoles.FindAsync(role.Id, user.Id);
                return userRole != null;
            }
            return false;
        }
        public async Task<bool> IsOrganizationMember(int userId, int organizationId)
        {
            return await _db.OrganizationMembers
                .Where(w => w.UserId == userId)
                .Where(w => w.OrganizationId == organizationId)
                .Where(w => w.IsActive)
                .AnyAsync();
        }
    }
}
