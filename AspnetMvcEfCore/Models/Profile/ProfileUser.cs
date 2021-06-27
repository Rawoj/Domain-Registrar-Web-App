using DomainRegistrarWebApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Models.Profile
{
    public class ProfileUser
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<BoughtDomain> BoughtDomains { get; set; }

    }
}
