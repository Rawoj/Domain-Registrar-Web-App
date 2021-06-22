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
    }
}