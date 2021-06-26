using DomainRegistrarWebApp.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainRegistrarWebApp.Database
{
    public class BoughtDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string TopLevelDomain { get; set; }

        [Required]
        public AppUser Owner { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }
    }
}