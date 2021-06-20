using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Database
{
    public class BoughtDomainsContext: DbContext
    {
        public BoughtDomainsContext(DbContextOptions options) : base(options) { }
        protected BoughtDomainsContext() { }
        public DbSet<BoughtDomain> BoughtDomains { get; set; }
    }
}
