using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AuthWithRoles.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AuthWithRoles.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            var _user = await UserManager.FindByEmailAsync(config.GetSection("AppSettings")["UserEmail"]);

            if (_user == null)
            {
                var poweruser = new ApplicationUser
                {
                    UserName = config.GetSection("AppSettings")["UserEmail"],
                    Email = config.GetSection("AppSettings")["UserEmail"]
                };

                string UserPassword = config.GetSection("AppSettings")["UserPassword"];

                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
