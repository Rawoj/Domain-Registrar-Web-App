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
        [HttpPost]
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

            var balance = user.Balance;

            // Provisional price
            decimal domainPrice = 10.00M;

            if (balance == null )//|| balance < domainPrice) 
            {
                // TODO: Show error, show controls to add funds
                return View();
            }
            else
            {
                var detailsModel = new PurchaseDetailsModel()
                {
                    DomainName = Name,
                    Price = domainPrice,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    UserBalance = balance
                };
                return View(detailsModel);
            }         
        }
    }
}
