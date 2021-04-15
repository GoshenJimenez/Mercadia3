using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain;
using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models;
using GoshenJimenez.Mercadia3.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly DefaultDbContext _context;
        protected readonly IConfiguration _config;
        private string emailUserName;
        private string emailPassword;
        public AccountController(DefaultDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            var emailConfig = this._config.GetSection("Email");
            emailUserName = (emailConfig["Username"]).ToString();
            emailPassword = (emailConfig["Password"]).ToString();
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
            this.SendNow(
                             "Hi " + user.FullName + @",
                             Welcome to Mercadia III. Please use this one-time password to login to your account: " + password + @".
                             Regards,
                             Mercadia III",
                             model.EmailAddress,
                             user.FullName,
                             "Welcome to Mercadia III!!!"
                        );

            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Login");
                return View(model);
            }

            User user = _context.Users.FirstOrDefault(u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower());

            if(user == null)
            {
                ModelState.AddModelError("Error", "Invalid Login");
                return View(model);
            }
            else
            {
                UserLogin userPassword = _context.UserLogins.FirstOrDefault(l =>
                                                                        l.UserId == user.Id
                                                                    &&  l.Key.ToLower() == "password"
                                                                    &&  l.Type == LoginType.Email
                                                                );


                UserLogin loginRetries = _context.UserLogins.FirstOrDefault(l =>
                                                                l.UserId == user.Id
                                                            && l.Key.ToLower() == "loginretries"
                                                            && l.Type == LoginType.General
                                                        );

                UserLogin loginStatus = _context.UserLogins.FirstOrDefault(l =>
                                                                l.UserId == user.Id
                                                            && l.Key.ToLower() == "loginstatus"
                                                            && l.Type == LoginType.General
                                                        );

                if (userPassword != null)
                {
                    if(BCrypt.BCryptHelper.CheckPassword(model.Password, userPassword.Value) == true)
                    {
                        loginRetries.Value = "0";
                        _context.SaveChanges();

                        if (loginStatus.Value.ToLower() == "active")
                        {
                            //SIGN IN
                        }else if(loginStatus.Value.ToLower() == "needstochangepassword")
                        {
                            //SIGN IN
                            RedirectToAction("ChangePassword");
                        }
                        else if (loginStatus.Value.ToLower() == "lockedout")
                        {
                            ModelState.AddModelError("Error", "Your account is locked out.");
                            return View(model);
                        }
                    }
                    else
                    {
                        var retries = (int.Parse(loginRetries.Value) + 1);
                        loginRetries.Value = retries.ToString();
                        _context.SaveChanges();

                        if (retries > 2)
                        {
                            loginStatus.Value = "LockedOut";

                            _context.SaveChanges();

                            ModelState.AddModelError("Error", "Your account is locked out.");
                            return View(model);
                        }
                    }
                }

                ModelState.AddModelError("Error", "Invalid Login.");
                return View(model);
            }
        }

        

        private Random random = new Random();
        private string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SendNow(string message, string messageTo, string messageName, string emailSubject)
        {
            var fromAddress = new MailAddress(emailUserName, "CSM Bataan Apps");
            string body = message;


            ///https://support.google.com/accounts/answer/6010255?hl=en
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, emailPassword),
                Timeout = 20000
            };

            var toAddress = new MailAddress(messageTo, messageName);

            smtp.Send(new MailMessage(fromAddress, toAddress)
            {
                Subject = emailSubject,
                Body = body,
                IsBodyHtml = true
            });
        }
    }
}
