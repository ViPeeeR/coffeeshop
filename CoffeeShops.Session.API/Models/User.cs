using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        public Role Role { get; set; }

        [Required]
        public string ContactId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string PassHash { get; set; }
    }    
}
