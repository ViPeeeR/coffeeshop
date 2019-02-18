using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShops.Common
{
    public class RegisterModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public string ContactId { get; set; }
    }
}
