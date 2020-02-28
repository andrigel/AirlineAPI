using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Identity;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitFlights(EFDBContext context)
        {
            Random rand = new Random();
            if (!context.Flights.Any())
            {
                context.Flights.Add(new Flight { From = "City1", To = "place1", Length = rand.Next() % 2500, PlacesCount = 10, PlacesReserved = 0, Price = rand.Next() % 1000, Start = DateTime.Now.AddDays(5)});
                context.Flights.Add(new Flight { From = "City2", To = "place2", Length = rand.Next() % 2500, PlacesCount = 10, PlacesReserved = 0, Price = rand.Next() % 1000, Start = new DateTime() });
                context.SaveChanges();
            }
        }
        public static async Task<bool> InitUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if(roleManager.Roles.ToList().Count == 0)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
                await roleManager.CreateAsync(new IdentityRole("admin"));
                await roleManager.CreateAsync(new IdentityRole("manager"));
            }
            if(userManager.Users.ToList().Count == 0)
            {
                await userManager.CreateAsync(new ApplicationUser { Email = "1", UserName = "1", Year = 1000, PremiumMarksCount = 500 },"1Qwer_");
                await userManager.CreateAsync(new ApplicationUser { Email = "2", UserName = "2", Year = 2000, PremiumMarksCount = 100000 }, "2Qwer_");
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync("1"), "manager");
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync("2"), "admin");
            }
            return true;
        }
    }
}