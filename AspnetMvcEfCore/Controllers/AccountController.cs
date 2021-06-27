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
            var usersData = usersDataService.GetUser(username);
            if(usersData == null)
            {
                return Redirect(Url.Action("LogOut", "Auth"));
            }
            var profileUser = new ProfileUser()
            {
                Username = usersData.Username,
                Email = usersData.Email,
                DateCreated = usersData.DateCreated,
                BoughtDomains = usersData.BoughtDomains,
                Balance = usersData.Balance,
            };

            return View(profileUser);
        }
    }
}
