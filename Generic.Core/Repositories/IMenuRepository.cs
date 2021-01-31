using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;

namespace Generic.Core.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<dynamic> GetMenuById(int id);
        Task<IEnumerable<Menu>> GetMenus();

        Task<IEnumerable<Menu>> GetMenusById(IEnumerable<int> id);
    }
}