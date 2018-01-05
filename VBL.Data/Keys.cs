using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class VBLDbContext
    {
        public void AssignKeys(ModelBuilder builder)
        {
            builder.Entity<OrganizationLocation>()
                .HasKey(k => new { k.OrganizationId, k.LocationId });

            builder.Entity<OrganizationMember>()
                .HasKey(k => new { k.OrganizationId, k.UserId });
        }
    }
}
