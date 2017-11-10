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
            builder.Entity<UserPhoneNumber>()
                .HasKey(k => new { k.PhoneNumberId, k.UserId });

            builder.Entity<UserEmail>()
                .HasKey(k => new { k.EmailId, k.UserId });

            builder.Entity<OrganizationUser>()
                .HasKey(k => new { k.OrganizationId, k.UserId });
        }
    }
}
