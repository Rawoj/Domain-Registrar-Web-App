using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DomainRegistrarWebApp.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected DatabaseContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BoughtDomain> BoughtDomains { get; set; }


    }
}