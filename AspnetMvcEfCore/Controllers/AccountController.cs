using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Interfaces;
using DomainRegistrarWebApp.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var username = User.Identity.Name;

            IUsersDataService usersDataService = new UsersDataService(_db);
            IBoughtDomainsDataService boughtDomainsDataService = new BoughtDomainsDataService(_db);
            var userData = usersDataService.GetUser(username);
            if(userData == null)
            {
                return Redirect(Url.Action("LogOut", "Auth"));
            }

            var boughtDomains = usersDataService.GetUserDomains(userData);

            var profileUser = new ProfileUser()
            {
                Username = userData.Username,
                Email = userData.Email,
                DateCreated = userData.DateCreated,
                BoughtDomains = boughtDomains,
                Balance = userData.Balance,
            };

            return View(profileUser);
        }
    }
}
