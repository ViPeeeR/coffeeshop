using CoffeeShops.Common;
using CoffeeShops.Session.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture
{
    public interface IOAuth
    {
        Task<AuthenticateModel> ValidateRefresh(string token);

        string AuthorizationCode(string clientId);

        Task<AuthenticateModel> CreateToken(string code, string clientSecret, string clientId);

        AuthenticateModel CreateToken(User user);
    }
}
