using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;

namespace Generic.Core.Services
{
    public interface IRoleService
    {
        Task<BaseResponse<IEnumerable<Role>>> GetRoles();
        Task<BaseResponse<string>> GetRoleByName(String name);
        Task<BaseResponse<Role>>  CreateRole(Role Role);
        Task<BaseResponse<Role>> UpdateRole(Role name , Role Role);
        Task<BaseResponse<Role>> GetRoleById(int id);
        //Task<BaseResponse<int>> GetRoleByNameToId(String name);

    }
}
