using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain;
using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models;
using GoshenJimenez.Mercadia3.Web.Infrastructure.Security;
using GoshenJimenez.Mercadia3.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
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
                LastName = model.LastName,
                Role = Role.Customer,
            };

            List<UserLogin> userLogins = new List<UserLogin>();

            var salt = BCrypt.BCryptHelper.GenerateSalt();
            var password = RandomString(6);
            var hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt);

            userLogins.Add(new UserLogin()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Key = "Password",
                Value = hashedPassword,
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
                Type = LoginType.General
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
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
                        if (loginRetries != null)
                        {
                            loginRetries.Value = "0";
                        }
                        _context.SaveChanges();

                        if (loginStatus.Value.ToLower() == "active")
                        {
                            //SIGN IN
                           await SignIn(user);
                           return RedirectPermanent("~/products/index");
                        }
                        else if(loginStatus.Value.ToLower() == "needstochangepassword")
                        {
                            //SIGN IN
                            await SignIn(user);
                            return RedirectToAction("ChangePassword");
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

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "All fields are required");
                return View(model);
            };

            if(model.ConfirmPassword != model.NewPassword)
            {
                ModelState.AddModelError("Error", "New password and Confirm password does not match.");
                return View(model);
            }

            User user = _context.Users.FirstOrDefault(u => u.Id == this.User.GetId());

            if(user == null)
            {
                ModelState.AddModelError("Error", "Invalid Login");
                return RedirectToAction("Login");
            }
            else
            {

                UserLogin userPassword = _context.UserLogins.FirstOrDefault(l =>
                                                                        l.UserId == user.Id
                                                                    && l.Key.ToLower() == "password"
                                                                    && l.Type == LoginType.Email
                                                                );

                if(userPassword != null)
                {
                    if (BCrypt.BCryptHelper.CheckPassword(model.OldPassword, userPassword.Value) == true)
                    {
                        UserLogin loginStatus = _context.UserLogins.FirstOrDefault(l =>
                                                           l.UserId == user.Id
                                                       && l.Key.ToLower() == "loginstatus"
                                                       && l.Type == LoginType.General
                                                   );
                        if(loginStatus!= null)
                        {
                            loginStatus.Value = "Active";
                        }

                        var salt = BCrypt.BCryptHelper.GenerateSalt();
                       
                        var hashedPassword = BCrypt.BCryptHelper.HashPassword(model.NewPassword, salt);

                        userPassword.Value = hashedPassword;

                        _context.SaveChanges();

                        return RedirectPermanent("~/products");
                    }
                }

                ModelState.AddModelError("Error", "Invalid Login");
                return RedirectToAction("Login");

            }

            return View(model);
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

        public async Task SignIn(User user)
        {
            var identity = new System.Security.Claims.ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            //WebUser.SetUser(user);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
