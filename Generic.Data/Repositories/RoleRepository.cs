using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Generic.Core.Models;
using Generic.Core.Repositories;
using Microsoft.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Identity;

namespace Generic.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;
        public RoleRepository(RoleManager<Role> roleManager, DataContext context )
                : base(context)
        { 
            _roleManager = roleManager;
            _context = context;
        }        
        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await _roleManager.Roles.AsNoTracking()
            .Select(role => new Role
            {
                Id = role.Id,
                Name = role.Name,
                Status = role.Status
            }).ToListAsync();

            return roles;
        }  

        public async Task<Role> GetRoleById(int id)
        {
            var userFrom = await _context.Roles.AsNoTracking()
            .Select(role => new Role
            {
                Id = role.Id
                ,Name = role.Name
                ,Status = role.Status
                ,Menus = (from roleMenu in role.RoleMenus
                         join menu in _context.Menu
                         on roleMenu.MenuId
                         equals menu.Id
                         where menu.Status == 1
                         select new Menu
                         {
                             Id = menu.Id
                             ,
                             Path = menu.Path
                             ,
                             Title = menu.Title
                             ,
                             Icon = menu.Icon
                             ,
                             ParentId = menu.ParentId
                             ,
                             Status = menu.Status
                         }).ToList()
                ,
                UnAssignedMenus = (from menu in _context.Menu
                                   where !role.RoleMenus.Any(rm => rm.MenuId == menu.Id)
                                   && menu.Status == 1
                                   select new Menu
                                   {
                                       Id = menu.Id
                                       ,Path = menu.Path
                                       ,Title = menu.Title
                                       ,Icon = menu.Icon
                                       ,ParentId = menu.ParentId
                                       ,Status = menu.Status
                                   }).ToList()
            })
            .FirstOrDefaultAsync(role => role.Id == id);

            return userFrom;
        }   
        public async Task<Role> CreateRole(Role role)
        {
            _roleManager.CreateAsync(role).Wait();
            return role; 
        }

        public async Task<string> GetRoleByName(string name)
        {
            var roleMngr = await _roleManager.Roles.AsNoTracking().Where(w =>
                 w.Name == name && w.Status == 1)
            .Select(r => r.Name)
            .FirstOrDefaultAsync();
            
            return roleMngr;
        }
    }
}

