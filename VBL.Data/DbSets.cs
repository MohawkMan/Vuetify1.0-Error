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
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationLocation> OrganizationLocations { get; set; }
        public DbSet<OrganizationMember> OrganizationMembers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
    }
}
