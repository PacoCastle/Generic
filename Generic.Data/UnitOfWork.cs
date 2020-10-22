using Generic.Core.Repositories;
using Generic.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private ClientRepository _clientRepository;

        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }

        public IClientRepository Clients => _clientRepository = _clientRepository ?? new ClientRepository(_context);

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
