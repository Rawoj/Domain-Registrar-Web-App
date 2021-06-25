using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Interfaces
{
    public class BoughtDomainsDataService : IBoughtDomainsDataService
    {
        private readonly ApplicationDbContext _db;

        public BoughtDomainsDataService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddBoughtDomain(BoughtDomain boughtDomain)
        {
            try
            {
                _db.BoughtDomains.Add(boughtDomain);
                
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task AddBoughtDomainsByTransaction(IEnumerable<BoughtDomain> boughtDomains)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                foreach (var d in boughtDomains)
                {
                    _db.BoughtDomains.Add(d);
                    await _db.SaveChangesAsync();
                }

                await _db.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _db.Database.RollbackTransactionAsync();
            }
        }

        public async Task<List<BoughtDomain>> GetBoughtDomains()
        {
            return await _db.BoughtDomains.ToListAsync();
        }
    }
}