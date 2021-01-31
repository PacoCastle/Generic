using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;

namespace Generic.Core.Services
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<User>>  Login(String userName, String password);
    }
}
