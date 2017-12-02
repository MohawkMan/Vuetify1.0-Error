using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class VBLDbContext
    {
        public void IdentityRename(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(i => {
                i.ToTable("Users");
                i.HasKey(x => x.Id);
            });
            builder.Entity<ApplicationRole>(i => {
                i.ToTable("Roles");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserRole<int>>(i => {
                i.ToTable("UserRoles");
                i.HasKey(x => new { x.RoleId, x.UserId });
            });
            builder.Entity<IdentityUserLogin<int>>(i => {
                i.ToTable("UserLogins");
                i.HasKey(x => new { x.ProviderKey, x.LoginProvider });
            });
            builder.Entity<IdentityRoleClaim<int>>(i => {
                i.ToTable("RoleClaims");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserClaim<int>>(i => {
                i.ToTable("UserClaims");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserToken<int>>(i =>
            {
                i.ToTable("UserTokens");
                i.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            });
        }
    }
}
