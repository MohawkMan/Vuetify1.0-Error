using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class VBLDbContext
    {
        public DbSet<AgeType> AgeTypes { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<FacebookProfile> FacebookProfiles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationLocation> OrganizationLocations { get; set; }
        public DbSet<OrganizationMember> OrganizationMembers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<SanctioningBody> SanctioningBodies { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentDay> TournamentDays { get; set; }
        public DbSet<TournamentDivision> TournamentDivisions { get; set; }
        public DbSet<TournamentRegistrationEmail> TournamentRegistrationEmails { get; set; }
        public DbSet<TournamentRegistrationInfo> TournamentRegistrationInfo { get; set; }
        public DbSet<TournamentRegistrationWindow> TournamentRegistrationWindows { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
    }
}
