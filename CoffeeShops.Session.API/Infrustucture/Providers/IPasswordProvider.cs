using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture.Providers
{
    public interface IPasswordProvider
    {
        string GetHash(string password, string salt);

        string GetRandomPassword(int length);

        bool VerifyHashedPassword(string hash, string salt, string password);
    }
}
