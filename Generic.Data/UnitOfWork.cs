using Generic.Core.Repositories;
using Generic.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Generic.Core.Models;

namespace Generic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private UserRepository _userRepository;
        private AuthenticationRepository _authenticationRepository;

        public UnitOfWork(DataContext context, RoleManager<Role> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_userManager, _context);
        public IAuthenticationRepository AuthenticationRepository => _authenticationRepository = _authenticationRepository ?? new AuthenticationRepository(_signInManager, _context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
