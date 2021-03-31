using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic;
using Proiect.Entities;
using Proiect.WebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserAccountService service;
        private readonly IMapper mapper;
        public RegisterController(UserAccountService service,
            IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegisterViewModel();
            model.IsPacient = bool.TrueString;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<Person>(model);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                if (service.CanRegister(user) == false)
                {
                    return View(model);
                }

                string tabela = "";
                if (bool.Parse(model.IsPacient))
                {
                    tabela = "Patient";
                }
                else
                {
                    tabela = "Medic";
                }
                service.RegisterNewUser(user, tabela);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
