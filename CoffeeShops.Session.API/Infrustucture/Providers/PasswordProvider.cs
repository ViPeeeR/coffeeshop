using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture.Providers
{
    public class PasswordProvider : IPasswordProvider
    {
        public string GetHash(string password, string salt)
        {
            var data = Encoding.UTF8.GetBytes(salt + password);
            var hash = SHA256.Create().ComputeHash(data);
            return Encoding.UTF8.GetString(hash);
        }

        public string GetRandomPassword(int length)
        {
            const string availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random rnd = new Random();
            return new string(Enumerable.Repeat(availableChars, length).Select(x => x[rnd.Next(x.Length)]).ToArray());
        }

        public bool VerifyHashedPassword(string hash, string salt, string password)
        {
            return hash == GetHash(password, salt);
        }
    }
}
