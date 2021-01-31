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
    public class RoleMenuRepository : Repository<RoleMenu>, IRoleMenuRepository
    {
        private readonly DataContext _context;    
         
        public RoleMenuRepository(DataContext context) 
            : base(context)
        { 
            _context = context;            
        }

        public async Task RemoveRoleId(int id)
        {
            var roleId = await _context.RoleMenu.Where(x => x.RoleId == id).ToListAsync();
             _context.RoleMenu.RemoveRange(roleId);
            await _context.SaveChangesAsync();
            
        }

    }
}

