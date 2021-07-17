using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DomainRegistrarWebApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Models.Users;
using DomainRegistrarWebApp.Controllers;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainRegistrarWebApp.Models.Search
{

    public class DomainPurchase
    {
        private readonly ApplicationDbContext _db;
        
        public DomainPurchase(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> BuyDomain(AppUser appUser, DomainInfo domainInfo, decimal price, DateTime expirationDate)
        {
            if (appUser == null || domainInfo == null)
                throw new Exception("Domain purchase failure: User or Domain is not instanced.");

            // Check if domain is still available
            var match = _db.BoughtDomains.FindAsync(new BoughtDomain() { Name = domainInfo.DomainName });
            if (await match != null)
            {
                // Domain taken
                return false; 
            }

            await _db.Database.BeginTransactionAsync();
            _db.Users.Update(appUser);
            if (appUser.Balance >= price)
            {
                appUser.Balance -= price;
            }
            else
            {
                // Not enough funds, maybe reroute to AddBalance page
                await _db.Database.RollbackTransactionAsync();
                return false;
            }
            var boughtDomain = new BoughtDomain()
            {
                Name = domainInfo.DomainName,
                Owner = appUser,
                PurchaseDate = DateTime.Now,
                ExpirationDate = expirationDate
            };

            _db.BoughtDomains.Add(boughtDomain);
            appUser.BoughtDomains.Add(boughtDomain);

            await _db.SaveChangesAsync();
            await _db.Database.CommitTransactionAsync();
            return true;

        }
    }
}
