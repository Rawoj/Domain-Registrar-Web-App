using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainRegistrarWebApp.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime DateCreated { get; set; }
    }
}