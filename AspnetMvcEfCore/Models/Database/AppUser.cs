using DomainRegistrarWebApp.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainRegistrarWebApp.Models.Users
{
    public class AppUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Aspnet user ID
        public string OwnerId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Balance { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(128, MinimumLength = 7)]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public ICollection<BoughtDomain> BoughtDomains { get; set; }


    }
}