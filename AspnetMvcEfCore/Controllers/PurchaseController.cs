using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Interfaces;
using DomainRegistrarWebApp.Models.Purchase;
using DomainRegistrarWebApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DomainRegistrarWebApp.Controllers;
using DomainRegistrarWebApp.Models.Search;

namespace DomainRegistrarWebApp.Controllers
{
    [Route("Purchase")]
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PurchaseController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpPost("Details")]
        [ValidateAntiForgeryToken]

        public IActionResult Index(string Name)
        {
            IUsersDataService userDataService = new UsersDataService(_db);

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var nameClaim = claimsIdentity.FindFirst("user");

            var user = userDataService.GetUser(new AppUser()
            {
                Username = nameClaim.Value
            });

            // Provisional price
            decimal domainPrice = 10.00M;

            // User not found
            if (user == default)
            {
                var url = Url.Action("Error", "Home");
                return Redirect(url);
            }
            else
            {
                var detailsModel = new PurchaseDetailsModel()
                {
                    DomainName = Name,
                    Price = domainPrice,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    UserBalance = user.Balance
                };
                
                return View(detailsModel);
            }         
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(PurchaseDetailsModel purchaseDetailsModel)
        {
            DomainPurchase domainPurchase = new DomainPurchase(_db);
            IUsersDataService userDataService = new UsersDataService(_db);         

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var nameClaim = claimsIdentity.FindFirst("user");

            var user = userDataService.GetUser(new AppUser()
            {
                Username = nameClaim.Value
            });

            var isPurchaseSuccessful = await domainPurchase.BuyDomain(
                user,
                new DomainInfo() { DomainName = purchaseDetailsModel.DomainName },
                0,
                DateTime.Now.AddMonths(1) // ExpirationDate
            );
            if (isPurchaseSuccessful)
            {
                var url = Url.Action("Profile", "Account");
                return Redirect(url);
            }
            else
            {
                var url = Url.Action("Error", "Home");
                return Redirect(url);
            }
            
        }
    }
}
