using Microsoft.EntityFrameworkCore;

namespace DomainRegistrarWebApp.Database
{
    public class BoughtDomainsContext : DbContext
    {
        public BoughtDomainsContext(DbContextOptions options) : base(options)
        {
        }

        protected BoughtDomainsContext()
        {
        }

        public DbSet<BoughtDomain> BoughtDomains { get; set; }
    }
}