using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }

        Task<int> CommitAsync();
    }
}
