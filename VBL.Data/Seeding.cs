using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VBL.Data
{
    public static class Seeding
    {
        public static async Task EnsureSeedData(this VBLDbContext db, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if(!await db.Users.AnyAsync(a => a.UserName == "2146748568"))
            {
                var admin = new ApplicationUser()
                {
                    FirstName = "Jon",
                    LastName = "Alvarez",
                    PhoneNumber = "2146748568",
                    Email = "Jon@VolleyballLife.com",
                    UserName = "2146748568"
                };

                var x = await userManager.CreateAsync(admin, "volley13");
                var y = await roleManager.CreateAsync(new ApplicationRole("MohawkMan"));
                var z = await roleManager.CreateAsync(new ApplicationRole("Admin"));

                var result = await userManager.AddToRoleAsync(admin, "MohawkMan");
            }

            await db.SeedAgeTypes();
            await db.SeedGenders();
            await db.SeedDivisions();
            await db.SeedLocations();
        }
        public static async Task<bool> SeedAgeTypes(this VBLDbContext db)
        {
            if (await db.AgeTypes.AnyAsync())
                return false;

            db.AgeTypes.Add(new AgeType { Name = "Juniors" });
            db.AgeTypes.Add(new AgeType { Name = "Adult" });
            await db.SaveChangesAsync();

            return true;
        }
        public static async Task<bool> SeedGenders(this VBLDbContext db)
        {
            if (await db.Genders.AnyAsync())
                return false;

            db.Genders.Add(new Gender { Name = "Mens", AgeTypeId = 2, Order = 1 });
            db.Genders.Add(new Gender { Name = "Womens", AgeTypeId = 2, Order = 2 });
            db.Genders.Add(new Gender { Name = "Coed", AgeTypeId = 2, Order = 3 });
            db.Genders.Add(new Gender { Name = "Boys", AgeTypeId = 1, Order = 4 });
            db.Genders.Add(new Gender { Name = "Girls", AgeTypeId = 1, Order = 5 });
            db.Genders.Add(new Gender { Name = "Coed", AgeTypeId = 1, Order = 6 });
            db.Genders.Add(new Gender { Name = "Boys or Coed", AgeTypeId = 1, Order = 7 });
            await db.SaveChangesAsync();

            return true;
        }
        public static async Task<bool> SeedDivisions(this VBLDbContext db)
        {
            if (await db.Divisions.AnyAsync())
                return false;

            db.Divisions.Add(new Division { Name = "AAA", AgeTypeId = 2, Order = 1 });
            db.Divisions.Add(new Division { Name = "AA", AgeTypeId = 2, Order =  2});
            db.Divisions.Add(new Division { Name = "A", AgeTypeId = 2, Order =  3});
            db.Divisions.Add(new Division { Name = "BB", AgeTypeId = 2, Order =  4});
            db.Divisions.Add(new Division { Name = "B", AgeTypeId = 2, Order =  5});
            db.Divisions.Add(new Division { Name = "Unrated", AgeTypeId = 2, Order =  6});
            db.Divisions.Add(new Division { Name = "Masters", AgeTypeId = 2, Order =  7});
            db.Divisions.Add(new Division { Name = "Senoirs", AgeTypeId = 2, Order =  8});

            db.Divisions.Add(new Division { Name = "10U", AgeTypeId = 1, Order =  1});
            db.Divisions.Add(new Division { Name = "12U", AgeTypeId = 1, Order =  2});
            db.Divisions.Add(new Division { Name = "14U", AgeTypeId = 1, Order =  3});
            db.Divisions.Add(new Division { Name = "16U", AgeTypeId = 1, Order =  4});
            db.Divisions.Add(new Division { Name = "18U", AgeTypeId = 1, Order =  5});
            db.Divisions.Add(new Division { Name = "17+/Open", AgeTypeId = 1, Order =  6});
            db.Divisions.Add(new Division { Name = "12U Rec*", AgeTypeId = 1, Order =  8});
            db.Divisions.Add(new Division { Name = "14U Rec*", AgeTypeId = 1, Order = 9 });
            db.Divisions.Add(new Division { Name = "16U Rec*", AgeTypeId = 1, Order = 10 });
            db.Divisions.Add(new Division { Name = "18U Rec*", AgeTypeId = 1, Order = 11 });
            db.Divisions.Add(new Division { Name = "18U/16U Elite", AgeTypeId = 1, Order =  12});
            await db.SaveChangesAsync();

            return true;
        }
        public static async Task<bool> SeedLocations(this VBLDbContext db)
        {
            if (await db.Locations.AnyAsync())
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
            await db.SaveChangesAsync();

            return true;
        }
    }
}
