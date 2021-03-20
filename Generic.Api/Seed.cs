using System.Collections.Generic;
using System.Linq;
using Generic.Core.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Generic.Api
{
    public class Seed
    {
        public static void SeedUsers(RoleManager<Role> roleManager)
        {
            var roles = new List<Role>
                {
                    new Role{Name = "Admin"}
                };

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            };
        }
    }
}