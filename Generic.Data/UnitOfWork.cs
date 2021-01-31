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
        private ClientRepository _clientRepository;

        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private MenuRepository _menuRepository;
        private RoleRepository _roleRepository;
        private UserRepository _userRepository;
        private AuthenticationRepository _authenticationRepository;
        private RoleMenuRepository _roleMenuRepository;

        public UnitOfWork(DataContext context, RoleManager<Role> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IClientRepository Clients => _clientRepository = _clientRepository ?? new ClientRepository(_context);

        public IMenuRepository MenuRepository => _menuRepository = _menuRepository ?? new MenuRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository = _roleRepository ?? new RoleRepository(_roleManager, _context);
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_userManager, _context);
        public IAuthenticationRepository AuthenticationRepository => _authenticationRepository = _authenticationRepository ?? new AuthenticationRepository(_signInManager, _context);

        public IRoleMenuRepository RoleMenuRepository => _roleMenuRepository = _roleMenuRepository ?? new RoleMenuRepository(_context);

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
