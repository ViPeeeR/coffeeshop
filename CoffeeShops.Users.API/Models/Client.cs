using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Users.API.Models
{
    public class Client
    {
        [Key]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public Sex Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Phone { get; set; }
    }
}
