using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;

namespace Generic.Core.Services
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<User>>> GetUsers();
        Task<BaseResponse<User>> GetUserById(int id);
        Task<BaseResponse<User>>  CreateUser(User user, String password);
        Task<BaseResponse<User>> UpdateUser(User UserToBeUpdateModel, User UserForUpdateModel, string NewPassword);
        Task<BaseResponse<User>> GetUserByUserName(string userName);

    }
}
