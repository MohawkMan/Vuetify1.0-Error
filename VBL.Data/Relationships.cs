using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class VBLDbContext
    {
        public void BuildRelationships(ModelBuilder builder)
        {
            #region Email
            builder.Entity<Email>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<Email>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region Organization
            builder.Entity<Organization>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<Organization>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region OrganizationUser
            builder.Entity<OrganizationUser>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<OrganizationUser>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region PhoneNumber
            builder.Entity<PhoneNumber>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<PhoneNumber>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region User
            builder.Entity<ApplicationUser>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<ApplicationUser>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region UserEmail
            builder.Entity<UserEmail>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<UserEmail>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            builder.Entity<UserEmail>().HasOne(a => a.User)
                .WithMany(u => u.UserEmails)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);
            #endregion

            #region UserNotification
            builder.Entity<UserNotification>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<UserNotification>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            builder.Entity<UserNotification>().HasOne(a => a.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);
            #endregion

            #region UserPhoneNumber
            builder.Entity<UserPhoneNumber>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<UserPhoneNumber>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            builder.Entity<UserPhoneNumber>().HasOne(a => a.User)
                .WithMany(u => u.UserPhoneNumbers)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);
            #endregion



        }
    }
}
