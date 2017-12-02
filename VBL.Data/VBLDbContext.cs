using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VBL.Data
{
    public partial class VBLDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        private readonly HttpContext Context;
        public VBLDbContext(DbContextOptions<VBLDbContext> options, IHttpContextAccessor contextAccessor = null) : base(options)
        {
            Context = contextAccessor?.HttpContext;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            IdentityRename(builder);
            AssignKeys(builder);
            BuildRelationships(builder);
        }
        public override int SaveChanges()
        {
            AddTracking();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTracking();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTracking(int userId = 0)
        {
            if(userId == 0)
            {
                IdentityOptions _options = new IdentityOptions();
                int.TryParse(Context?.User?.FindFirstValue(_options.ClaimsIdentity.UserIdClaimType), out userId);
            }

            var entities = ChangeTracker.Entries().Where(x => x.Entity is ITrackedEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            if (!entities.Any())
                return;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    if (!((ITrackedEntity)entity.Entity).DtCreated.HasValue)
                        ((ITrackedEntity)entity.Entity).DtCreated = DateTime.UtcNow;

                    if(userId > 0)
                        ((ITrackedEntity)entity.Entity).UserIdCreated = userId;
                }

                ((ITrackedEntity)entity.Entity).DtModified = DateTime.UtcNow;
                if (userId > 0)
                    ((ITrackedEntity)entity.Entity).UserIdModified = userId;
            }
        }
    }
}
