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
        public DbSet<FacebookProfile> FacebookProfiles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationLocation> OrganizationLocations { get; set; }
        public DbSet<OrganizationMember> OrganizationMembers { get; set; }
        public DbSet<PayPalPaymentResponse> PayPalPaymentResponses { get; set; }
        public DbSet<PayPalToken> PayPalTokens { get; set; }
        public DbSet<PayPalTransaction> PayPalTransactions { get; set; }
        public DbSet<PlayerProfile> PlayerProfiles { get; set; }
        public DbSet<PointValue> PointValues { get; set; }
        public DbSet<PointValueMultiplier> PointValueMultipliers { get; set; }
        public DbSet<SanctioningBody> SanctioningBodies { get; set; }
        public DbSet<ShoppingBag> ShoppingBags { get; set; }
        public DbSet<ShoppingBagItem> ShoppingBagItems { get; set; }
        public DbSet<SparkPostEmailTemplate> SparkPostEmailTemplates { get; set; }
        public DbSet<StripeAccountDetails> StripeAccountDetails { get; set; }
        public DbSet<StripeAuthToken> StripeAuthTokens { get; set; }
        public DbSet<StripeConnectClick> StripeConnectClicks { get; set; }
        public DbSet<StripeChargeRecord> StripeChargeRecords { get; set; }
        public DbSet<StripePaymentToken> StripePaymentTokens { get; set; }
        public DbSet<TeamCountMultiplier> TeamCountMultipliers { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentDay> TournamentDays { get; set; }
        public DbSet<TournamentDivision> TournamentDivisions { get; set; }
        public DbSet<TournamentRegistration> TournamentRegistrations { get; set; }
        public DbSet<TournamentRegistrationEmail> TournamentRegistrationEmails { get; set; }
        public DbSet<TournamentRegistrationInfo> TournamentRegistrationInfo { get; set; }
        public DbSet<TournamentRegistrationPlayer> TournamentRegistrationPlayers { get; set; }
        public DbSet<TournamentRegistrationWindow> TournamentRegistrationWindows { get; set; }
        public DbSet<TournamentTeam> TournamentTeams { get; set; }
        public DbSet<TournamentTeamMember> TournamentTeamMembers { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
    }
}
