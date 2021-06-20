using DomainRegistrarWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Database
{
    public class BoughtDomain
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string TopLevelDomain { get; set; }

        public User Owner { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
