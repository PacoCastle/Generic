using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IAuthenticationRepository AuthenticationRepository { get; }
        Task<int> CommitAsync();
    }
}
