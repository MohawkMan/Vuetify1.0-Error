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
            builder.Entity<UserPhone>()
                .HasKey(k => new { k.PhoneId, k.UserId });

            builder.Entity<UserEmail>()
                .HasKey(k => new { k.EmailId, k.UserId });

            builder.Entity<OrganizationLocation>()
                .HasKey(k => new { k.OrganizationId, k.LocationId });

            builder.Entity<OrganizationMember>()
                .HasKey(k => new { k.OrganizationId, k.UserId });
        }
    }
}
