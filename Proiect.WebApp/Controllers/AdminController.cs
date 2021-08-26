using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic;
using Proiect.DataAccess.EntityFramework;
using Proiect.Entities;
using Proiect.WebApp.Models;
using Proiect.WebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly LoginViewModel loginViewModel;
        private readonly ImageService imageService;
        private readonly SpecializationService specializationService;
        private readonly MedicService medicService;
        private readonly PatientService patientService;
        public AdminController(LoginViewModel loginViewModel,
            ImageService imageService,
            SpecializationService specializationService,
            MedicService medicService,
            PatientService patientService)
        {
            this.imageService = imageService;
            this.specializationService = specializationService;
            this.medicService = medicService;
            this.loginViewModel = loginViewModel;
            this.patientService = patientService;
        }

        [HttpGet]
        public IActionResult Index(Guid idPerson)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString && loginViewModel.Id == idPerson.ToString())
            {
                if (loginViewModel.IsAdmin)
                {
                    var viewModel = new AdminViewModel
                    {
                        Medics = medicService.GetMedics(),
                        Patients = patientService.GetAllPatients()
                    };
                    return View(viewModel);
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                return View("NoAccessPage");
            }
        }

        public IActionResult ApproveMedic(Guid idMedic)
        {
            if (loginViewModel.IsLogedIn)
            {
                if (loginViewModel.IsAdmin)
                {
                    medicService.ApproveMedic(idMedic);
                    return RedirectToAction("Index", "Admin", new { idPerson = Guid.Parse(loginViewModel.Id) });
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                return View("NoAccessPage");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminViewModel model)
        {
            if (loginViewModel.IsLogedIn)
            {
                if (loginViewModel.IsAdmin)
                {
                    var image = new Image();
                    if (ModelState.IsValid && model.Image!=null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.Image.CopyToAsync(memoryStream);
                            image._Image = memoryStream.ToArray();

                        }
                        image.MimeType = model.Image.ContentType;

                        image.Description = model.Description;

                        Specialization specialization = new Specialization
                        {
                            Id = Guid.NewGuid(),
                            Name = model.Name
                        };

                        image = imageService.InsertImage(image);
                        specialization.IdImage = image.Id;
                        specialization.Image = image;
                        specializationService.AddSpecialization(specialization);

                        return View();
                    }
                    else
                    {
                        model.Medics = medicService.GetMedics();
                        model.Patients = patientService.GetAllPatients();
                        return View(model);
                    }
                    
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                return View("NoAccessPage");
            }

        }

        public IActionResult DeleteMedic(Guid idMedic)
        {
            if (loginViewModel.IsLogedIn)
            {
                if (loginViewModel.IsAdmin)
                {
                    medicService.DeleteMedicProfile(idMedic);
                    return RedirectToAction("Index", new { idPerson=Guid.Parse(loginViewModel.Id)});
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                return View("NoAccessPage");
            }
        }

        public IActionResult DeletePatient(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn)
            {
                if (loginViewModel.IsAdmin)
                {
                    patientService.DeletePatientProfile(idPatient);
                    return RedirectToAction("Index", new { idPerson = Guid.Parse(loginViewModel.Id) });
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                return View("NoAccessPage");
            }
        }
    }
}
