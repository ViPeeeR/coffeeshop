using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Services
{
    public interface IAuthService
    {
        Task<AuthenticateModel> Login(LoginModel model);

        Task<AuthenticateModel> Register(RegisterModel model);

        Task<string> AuthCode(string clientId);

        Task<AuthenticateModel> Login(string clientId, string clientSecret, string code);

        Task<AuthenticateModel> Refresh(AuthenticateModel model);
    }
}
