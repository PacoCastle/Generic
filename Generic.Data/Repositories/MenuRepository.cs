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
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {

        private readonly DataContext _context;

        public MenuRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }


        public async Task<dynamic> GetMenuById(int id)
        {
            //Menu m = new Menu();
            var menu = await _context.Menu
               .Select(menu => new
               {
                   Id = menu.Id
                   ,
                   Title = menu.Title
                   ,
                   Path = menu.Path
                   ,
                   Status = menu.Status
                   ,
                   Icon = menu.Icon
                   ,
                   ParentId = menu.ParentId

               }).FirstOrDefaultAsync();

            return menu;

        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {

            var menuList = await _context.Menu
                .OrderBy(x => x.Title)
                .Select(menu => new
                {
                    Id = menu.Id
                    ,
                    Title = menu.Title
                    ,
                    Path = menu.Path
                    ,
                    Status = menu.Status
                    ,
                    Icon = menu.Icon
                    ,
                    ParentId = menu.ParentId
                }).ToListAsync();

            return (IEnumerable<Menu>)menuList;

        }

        public async Task<IEnumerable<Menu>> GetMenusById(IEnumerable<int> idMenu)
        {
            var menu = await _context.Menu
               .Where(menu => idMenu.Contains(menu.Id))
               .Where(menu => menu.Status == 1)
               .Select(menu => new Menu
               {
                   Id = menu.Id
                   ,
                   Title = menu.Title
                   ,
                   Path = menu.Path
                   ,
                   Status = menu.Status
                   ,
                   Icon = menu.Icon
                   ,
                   ParentId = menu.ParentId

               }).ToListAsync();

            return menu;

        }
    }
}

