using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic;
using Proiect.Common;
using Proiect.Entities;
using Proiect.WebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Proiect.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserAccountService accountService;
        public LoginController(UserAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new LoginViewModel();
            model.IsPacient = bool.TrueString;
            model.IsAdmin = false;
            model.IsValid = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (bool.Parse(model.IsPacient))
                {
                    var pacient = accountService.LoginPatient(model.Email);
                    if (pacient == null)
                    {
                        model.IsValid = false;
                        return View(model);
                    }
                    else
                    {
                        var canLogin = BCrypt.Net.BCrypt.Verify(model.Password, pacient.Person.Password);
                        if (canLogin)
                        {
                            var user = pacient.Person;
                            await LogIn(user, "Patient");
                            model.Password = null;
                            return RedirectToAction("Index", "Patient", new { idPerson = user.Id });
                        }
                        else
                        {
                            model.IsValid = false;
                            return View(model);
                        }
                    }
                }
                else
                {
                    var medic = accountService.LoginMedic(model.Email);                   
                    if (medic == null)
                    {
                        model.IsValid = false;
                        return View(model);
                    }
                    else
                    {
                        var canLogin = BCrypt.Net.BCrypt.Verify(model.Password, medic.Person.Password);
                        if (canLogin)
                        {
                            var user = medic.Person;
                            if (user.IsAdmin)
                            {
                                model.IsAdmin = user.IsAdmin;
                            }
                            await LogIn(user, "Medic");
                            model.Password = null;
                            return RedirectToAction("Index", "Medic", new { idPerson = user.Id });
                        }
                        else
                        {
                            model.IsValid = false;
                            return View(model);
                        }
                    }
                }
            }
            return View(model);
        }

        private async Task LogIn(Person user, string rol)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.LastName} {user.FirstName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,rol),
                new Claim(ClaimTypes.Surname,user.IsAdmin.ToString())
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "ProiectCookies",
                    principal: principal);
        }

        public async Task<IActionResult> Logout()
        {
            await LogOut();

            return RedirectToAction("Index", "Home");
        }

        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "ProiectCookies");
        }
    }
}
