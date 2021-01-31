using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Generic.Core.Repositories
{
    public interface IAuthenticationRepository 
    {
        Task<SignInResult> CheckPasswordSignIn(User user, string password);
    }
}