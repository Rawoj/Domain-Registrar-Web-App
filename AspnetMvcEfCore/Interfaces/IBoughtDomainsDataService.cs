using DomainRegistrarWebApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    interface IBoughtDomainsDataService
    {
        Task<List<BoughtDomain>> GetBoughtDomains();
        Task<bool> AddBoughtDomain(BoughtDomain d);
        Task AddBoughtDomainsByTransaction(IEnumerable<BoughtDomain> boughtDomains);

    }
}
