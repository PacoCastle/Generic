using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }

        IMenuRepository MenuRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IAuthenticationRepository AuthenticationRepository { get; }
        IRoleMenuRepository RoleMenuRepository { get; }

        Task<int> CommitAsync();
    }
}
