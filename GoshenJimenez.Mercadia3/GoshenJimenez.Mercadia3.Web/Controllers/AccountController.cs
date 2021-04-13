using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain;
using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models;
using GoshenJimenez.Mercadia3.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly DefaultDbContext _context;
        public AccountController(DefaultDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Required");
                return View(model);
            }

            User user = new User()
            {
                Id = Guid.NewGuid(),
                EmailAddress = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            List<UserLogin> userLogins = new List<UserLogin>();

            var salt = BCrypt.BCryptHelper.GenerateSalt();
            var password = BCrypt.BCryptHelper.HashPassword(RandomString(6), salt);

            userLogins.Add(new UserLogin()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Key = "Password",
                Value = password,
                Type = LoginType.Email
            });

            userLogins.Add(new UserLogin()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Key = "LoginStatus",
                Value = "NeedsToChangePassword",
                Type = LoginType.General
            });

            userLogins.Add(new UserLogin()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Key = "LoginRetries",
                Value = "0",
                Type = LoginType.Email
            });

            _context.Users.Add(user);
            _context.UserLogins.AddRange(userLogins);
            _context.SaveChanges();

            //SEND EMAIL

            return RedirectToAction("Login");
        }

        private Random random = new Random();
        private string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
