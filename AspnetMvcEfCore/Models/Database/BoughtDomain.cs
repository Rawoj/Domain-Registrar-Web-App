using DomainRegistrarWebApp.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

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