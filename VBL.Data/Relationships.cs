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

            #region EmailMessage
            builder.Entity<EmailMessage>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<EmailMessage>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region Location
            builder.Entity<Location>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<Location>().HasOne(a => a.UserModified)
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

            #region OrganizationLocation
            builder.Entity<OrganizationLocation>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<OrganizationLocation>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region OrganizationMember
            builder.Entity<OrganizationMember>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<OrganizationMember>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region Phone
            builder.Entity<Phone>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<Phone>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region SanctioningBody
            builder.Entity<SanctioningBody>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<SanctioningBody>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region Tournament
            builder.Entity<Tournament>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<Tournament>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region TournamentDay
            builder.Entity<TournamentDay>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<TournamentDay>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region TournamentDivision
            builder.Entity<TournamentDivision>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<TournamentDivision>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region TournamentRegistrationEmail
            builder.Entity<TournamentRegistrationEmail>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<TournamentRegistrationEmail>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region TournamentRegistrationWindow
            builder.Entity<TournamentRegistrationWindow>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<TournamentRegistrationWindow>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region TournamentTeam
            builder.Entity<TournamentTeam>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<TournamentTeam>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            #endregion

            #region TournamentTeamMember
            builder.Entity<TournamentTeamMember>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<TournamentTeamMember>().HasOne(a => a.UserModified)
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

            #region UserPhone
            builder.Entity<UserPhone>().HasOne(a => a.UserCreated)
                .WithMany()
                .HasForeignKey(a => a.UserIdCreated)
                .IsRequired(false);
            builder.Entity<UserPhone>().HasOne(a => a.UserModified)
                .WithMany()
                .HasForeignKey(a => a.UserIdModified)
                .IsRequired(false);
            builder.Entity<UserPhone>().HasOne(a => a.User)
                .WithMany(u => u.UserPhones)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);
            #endregion
        }
    }
}
