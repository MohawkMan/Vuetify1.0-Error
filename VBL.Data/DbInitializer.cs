using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBL.Data
{
    public class DbInitializer
    {
        public static async Task Initialize (VBLDbContext db, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<DbInitializer> logger)
        {
            db.Database.EnsureCreated();
            await SeedUsers(userManager, roleManager);

            var a = SeedAgeTypes(db);
            var b = SeedGenders(db);
            var c = SeedDivisions(db);
            var d = SeedLocations(db);
            var e = SeedPoints(db);
            var f = SeedTeamMultiplier(db);

            if (a || b || c || d || e || f)
                await db.SaveChangesAsync();
        }

        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var roles = new List<string>() { "MohawkMan", "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new ApplicationRole(role));
            }

            if (await userManager.FindByNameAsync("2146748568") == null)
            {
                var me = new ApplicationUser()
                {
                    FirstName = "Jon",
                    LastName = "Alvarez",
                    PhoneNumber = "2146748568",
                    Email = "Jon@VolleyballLife.com",
                    UserName = "2146748568"
                };

                var user = await userManager.CreateAsync(me, "volley13");
                if(user.Succeeded)
                    await userManager.AddToRoleAsync(me, "MohawkMan");
            }
        }
        public static bool SeedAgeTypes(VBLDbContext db)
        {
            if (db.AgeTypes.Any())
                return false;

            db.AgeTypes.Add(new AgeType { Name = "Juniors" });
            db.AgeTypes.Add(new AgeType { Name = "Adult" });

            return true;
        }
        public static bool SeedGenders(VBLDbContext db)
        {
            if (db.Genders.Any())
                return false;

            db.Genders.Add(new Gender { Name = "Mens", AgeTypeId = 2, Order = 1 });
            db.Genders.Add(new Gender { Name = "Womens", AgeTypeId = 2, Order = 2 });
            db.Genders.Add(new Gender { Name = "Coed", AgeTypeId = 2, Order = 3 });
            db.Genders.Add(new Gender { Name = "Boys", AgeTypeId = 1, Order = 4 });
            db.Genders.Add(new Gender { Name = "Girls", AgeTypeId = 1, Order = 5 });
            db.Genders.Add(new Gender { Name = "Coed", AgeTypeId = 1, Order = 6 });
            db.Genders.Add(new Gender { Name = "Boys or Coed", AgeTypeId = 1, Order = 7 });

            return true;
        }
        public static bool SeedDivisions(VBLDbContext db)
        {
            if (db.Divisions.Any())
                return false;

            db.Divisions.Add(new Division { Name = "AAA", AgeTypeId = 2, Order = 1 });
            db.Divisions.Add(new Division { Name = "AA", AgeTypeId = 2, Order = 2 });
            db.Divisions.Add(new Division { Name = "A", AgeTypeId = 2, Order = 3 });
            db.Divisions.Add(new Division { Name = "BB", AgeTypeId = 2, Order = 4 });
            db.Divisions.Add(new Division { Name = "B", AgeTypeId = 2, Order = 5 });
            db.Divisions.Add(new Division { Name = "Unrated", AgeTypeId = 2, Order = 6 });
            db.Divisions.Add(new Division { Name = "Masters", AgeTypeId = 2, Order = 7 });
            db.Divisions.Add(new Division { Name = "Senoirs", AgeTypeId = 2, Order = 8 });

            db.Divisions.Add(new Division { Name = "10U", AgeTypeId = 1, Order = 1 });
            db.Divisions.Add(new Division { Name = "12U", AgeTypeId = 1, Order = 2 });
            db.Divisions.Add(new Division { Name = "14U", AgeTypeId = 1, Order = 3 });
            db.Divisions.Add(new Division { Name = "16U", AgeTypeId = 1, Order = 4 });
            db.Divisions.Add(new Division { Name = "18U", AgeTypeId = 1, Order = 5 });
            db.Divisions.Add(new Division { Name = "17+/Open", AgeTypeId = 1, Order = 6 });
            db.Divisions.Add(new Division { Name = "12U Rec*", AgeTypeId = 1, Order = 8 });
            db.Divisions.Add(new Division { Name = "14U Rec*", AgeTypeId = 1, Order = 9 });
            db.Divisions.Add(new Division { Name = "16U Rec*", AgeTypeId = 1, Order = 10 });
            db.Divisions.Add(new Division { Name = "18U Rec*", AgeTypeId = 1, Order = 11 });
            db.Divisions.Add(new Division { Name = "18U/16U Elite", AgeTypeId = 1, Order = 12 });

            return true;
        }
        public static bool SeedLocations(VBLDbContext db)
        {
            if (db.Locations.Any())
                return false;

            db.Locations.Add(new Location { Name = "Beach Blvd, Huntington Beach" });
            db.Locations.Add(new Location { Name = "Belmont Shore, Long Beach" });
            db.Locations.Add(new Location { Name = "Central Beach, Coronado" });
            db.Locations.Add(new Location { Name = "Doheny State Beach" });
            db.Locations.Add(new Location { Name = "East Beach, Santa Barbara" });
            db.Locations.Add(new Location { Name = "East Beach, Santa Barbara (Jrs.)" });
            db.Locations.Add(new Location { Name = "Hermosa Beach Pier" });
            db.Locations.Add(new Location { Name = "Huntington Beach Pier" });
            db.Locations.Add(new Location { Name = "Huntington State Beach (Newland)" });
            db.Locations.Add(new Location { Name = "Laguna Beach" });
            db.Locations.Add(new Location { Name = "Long Beach (Downtown)" });
            db.Locations.Add(new Location { Name = "Manhattan Beach Pier" });
            db.Locations.Add(new Location { Name = "Marine Street, Manhattan Beach" });
            db.Locations.Add(new Location { Name = "Marine Street, Manhattan Beach (Jrs.)" });
            db.Locations.Add(new Location { Name = "Mission Beach, San Diego" });
            db.Locations.Add(new Location { Name = "Monterey Bay (Waterfront Park)" });
            db.Locations.Add(new Location { Name = "Ocean Beach, San Diego" });
            db.Locations.Add(new Location { Name = "Ocean Beach, San Diego (Jrs.)" });
            db.Locations.Add(new Location { Name = "Ocean Park, Santa Monica" });
            db.Locations.Add(new Location { Name = "Pacific Palisades" });
            db.Locations.Add(new Location { Name = "Pismo Beach" });
            db.Locations.Add(new Location { Name = "Rosecrans, Manhattan Beach" });
            db.Locations.Add(new Location { Name = "Santa Cruz" });
            db.Locations.Add(new Location { Name = "Santa Monica North" });
            db.Locations.Add(new Location { Name = "Will Rogers State Beach" });
            db.Locations.Add(new Location { Name = "Zuma Beach, Malibu" });

            return true;
        }
        public static bool SeedPoints(VBLDbContext db)
        {
            if (db.PointValues.Any())
                return false;

            var updateMade = false;

            var division12 = db.Divisions.FirstOrDefault(f => f.Name == "12U");
            if (division12 != null)
            {
                db.PointValues.Add(new PointValue { Finish = 1, Division = division12, Points = 100 });
                db.PointValues.Add(new PointValue { Finish = 2, Division = division12, Points = 75 });
                db.PointValues.Add(new PointValue { Finish = 3, Division = division12, Points = 60 });
                db.PointValues.Add(new PointValue { Finish = 4, Division = division12, Points = 50 });
                db.PointValues.Add(new PointValue { Finish = 5, Division = division12, Points = 35 });
                db.PointValues.Add(new PointValue { Finish = 7, Division = division12, Points = 20 });
                db.PointValues.Add(new PointValue { Finish = 9, Division = division12, Points = 15 });
                db.PointValues.Add(new PointValue { Finish = 0, Division = division12, Points = 5 });
                updateMade = true;
            }

            var division14 = db.Divisions.FirstOrDefault(f => f.Name == "14U");
            if (division14 != null)
            {
                db.PointValues.Add(new PointValue { Finish = 1, Division = division14, Points = 100 * 2 });
                db.PointValues.Add(new PointValue { Finish = 2, Division = division14, Points = 75 * 2 });
                db.PointValues.Add(new PointValue { Finish = 3, Division = division14, Points = 60 * 2 });
                db.PointValues.Add(new PointValue { Finish = 4, Division = division14, Points = 50 * 2 });
                db.PointValues.Add(new PointValue { Finish = 5, Division = division14, Points = 35 * 2 });
                db.PointValues.Add(new PointValue { Finish = 7, Division = division14, Points = 20 * 2 });
                db.PointValues.Add(new PointValue { Finish = 9, Division = division14, Points = 15 * 2 });
                db.PointValues.Add(new PointValue { Finish = 0, Division = division14, Points = 5 });
                updateMade = true;
            }

            var division16 = db.Divisions.FirstOrDefault(f => f.Name == "16U");
            if (division16 != null)
            {
                db.PointValues.Add(new PointValue { Finish = 1, Division = division16, Points = 100 * 3 });
                db.PointValues.Add(new PointValue { Finish = 2, Division = division16, Points = 75 * 3 });
                db.PointValues.Add(new PointValue { Finish = 3, Division = division16, Points = 60 * 3 });
                db.PointValues.Add(new PointValue { Finish = 4, Division = division16, Points = 50 * 3 });
                db.PointValues.Add(new PointValue { Finish = 5, Division = division16, Points = 35 * 3 });
                db.PointValues.Add(new PointValue { Finish = 7, Division = division16, Points = 20 * 3 });
                db.PointValues.Add(new PointValue { Finish = 9, Division = division16, Points = 15 * 3 });
                db.PointValues.Add(new PointValue { Finish = 0, Division = division16, Points = 5 });
                updateMade = true;
            }

            var division18 = db.Divisions.FirstOrDefault(f => f.Name == "18U");
            if (division18 != null)
            {
                db.PointValues.Add(new PointValue { Finish = 1, Division = division18, Points = 100 * 4 });
                db.PointValues.Add(new PointValue { Finish = 2, Division = division18, Points = 75 * 4 });
                db.PointValues.Add(new PointValue { Finish = 3, Division = division18, Points = 60 * 4 });
                db.PointValues.Add(new PointValue { Finish = 4, Division = division18, Points = 50 * 4 });
                db.PointValues.Add(new PointValue { Finish = 5, Division = division18, Points = 35 * 4 });
                db.PointValues.Add(new PointValue { Finish = 7, Division = division18, Points = 20 * 4 });
                db.PointValues.Add(new PointValue { Finish = 9, Division = division18, Points = 15 * 4 });
                db.PointValues.Add(new PointValue { Finish = 0, Division = division18, Points = 5 });
                updateMade = true;
            }

            return updateMade;
        }
        public static bool SeedTeamMultiplier(VBLDbContext db)
        {
            if (db.TeamCountMultipliers.Any())
                return false;

            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 4,
                Multiplier = .8,
                Description = "Less than 5 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 9,
                Multiplier = 1,
                Description = "5 - 9 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 19,
                Multiplier = 1.1,
                Description = "10 - 19 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 29,
                Multiplier = 1.2,
                Description = "20 - 29 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 39,
                Multiplier = 1.3,
                Description = "30 - 39 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 49,
                Multiplier = 1.4,
                Description = "40 - 49 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 59,
                Multiplier = 1.5,
                Description = "50 - 59 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 69,
                Multiplier = 1.6,
                Description = "60 - 69 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 79,
                Multiplier = 1.7,
                Description = "70 - 79 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 89,
                Multiplier = 1.8,
                Description = "80 - 89 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 99,
                Multiplier = 1.9,
                Description = "90 - 99 teams"
            });
            db.TeamCountMultipliers.Add(new TeamCountMultiplier
            {
                TeamCap = 999999999,
                Multiplier = 2,
                Description = "100 or more teams"
            });

            return true;
        }
    }
}
