using DomainRegistrarWebApp.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    internal interface IBoughtDomainsDataService
    {
        Task<List<BoughtDomain>> GetBoughtDomains();

        Task<bool> AddBoughtDomain(BoughtDomain d);

        Task AddBoughtDomainsByTransaction(IEnumerable<BoughtDomain> boughtDomains);
        BoughtDomain GetBoughtDomain(BoughtDomain boughtDomain);

        Task<BoughtDomain> GetBoughtDomainAsync(BoughtDomain boughtDomain);

    }
}