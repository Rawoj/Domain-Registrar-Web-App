using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;


namespace DomainRegistrarWebApp.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BoughtDomain> BoughtDomains { get; set; }


    }
}