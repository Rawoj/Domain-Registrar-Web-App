using DomainRegistrarWebApp.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainRegistrarWebApp.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }


        // Aspnet user ID
        public string OwnerId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public ICollection<BoughtDomain> BoughtDomains { get; set; }


    }
}