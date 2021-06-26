using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Interfaces;
using DomainRegistrarWebApp.Models;
using DomainRegistrarWebApp.Models.Users;
using DomainRegistrarWebApp.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private bool ValidateLogin(string userName, string password)
        {
            IUsersDataService usersContext = new UsersDataService(_db);
            User user = new()
            {
                Username = userName,
               
            };
            User match = usersContext.GetUser(user);
            if (match == null)
            {
                return false;
            }
            var arePasswordsMatching = PasswordUtils.ComparePasswords(password, match.Password);

            return arePasswordsMatching;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
      
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (ValidateLogin(userName, password))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", userName),
                    new Claim("role", "Member")
                };

                await HttpContext.SignInAsync(
                    new ClaimsPrincipal(
                        new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            "user",
                            "role"
                            )
                        )
                    );

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/Account/Profile");
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Username, Email, Password")] User u)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            IUsersDataService usersContext = new UsersDataService(_db);
            u.DateCreated = System.DateTime.Now;
            u.Balance = 0;
            u.Password = PasswordUtils.GeneratePasswordHash(u.Password);
            var isRegistered = await usersContext.AddUser(u);
            if (isRegistered)
            {
                // If registered correctly, automatically log in and go to the profile
                return await Login(u.Username, u.Password, "/Account/Profile");
            }
            else
            {
                return View();
            }      
            
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View(model: User);
        }

        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
