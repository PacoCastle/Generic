using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;

namespace Generic.Core.Repositories
{
    public interface IRoleMenuRepository : IRepository<RoleMenu>
    {
        Task RemoveRoleId(int id);
    }
}