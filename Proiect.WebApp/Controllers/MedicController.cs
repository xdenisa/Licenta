using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Proiect.BusinessLogic;
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
    public class MedicController : Controller
    {
        private readonly LoginViewModel loginViewModel;
        private readonly MedicService medicService;
        private readonly SpecializationService specializationService;
        private readonly ImageService imageService;
        private readonly AppointmentService appointmentService;
        private readonly PatientService patientService;
        private readonly MedicineService medicineService;
        private readonly PortfolioService portfolioService;
        private readonly IMapper mapper;
        public MedicController(LoginViewModel loginViewModel,
            MedicService medicService,
            SpecializationService specializationService,
            ImageService imageService,
            AppointmentService appointmentService,
            PatientService patientService,
            MedicineService medicineService,
            PortfolioService portfolioService,
            IMapper mapper)
        {
            this.medicService = medicService;
            this.specializationService = specializationService;
            this.imageService = imageService;
            this.appointmentService = appointmentService;
            this.mapper = mapper;
            this.patientService = patientService;
            this.portfolioService = portfolioService;
            this.medicineService = medicineService;
            this.loginViewModel = loginViewModel;
        }
        public IActionResult Index(Guid idPerson)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString && loginViewModel.Id == idPerson.ToString())
            {
                var medic = medicService.GetMedicByPersonID(idPerson);
                var viewModel = mapper.Map<MedicViewModel>(medic);
                return View(viewModel);
            }
            else
            {
                return View("NoAccessPage");
            }
        }

        public IActionResult Admin(Guid idPerson)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString && loginViewModel.Id == idPerson.ToString())
            {
                return RedirectToAction("Index", "Admin", new { idPerson = idPerson });
            }
            else
            {
                return View("NoAccessPage");
            }
        }

        public IActionResult Patients(Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                var medic = medicService.GetMedicByID(idMedic.ToString());
                if (medic.IdPerson.ToString() == loginViewModel.Id)
                {
                    var appointments = appointmentService.GetAppointmentsByMedicId(idMedic)
                    .Where(d => d.AppointmentDate < DateTime.Now);
                    var viewModel = mapper.Map<MedicViewModel>(medic);

                    var appointmentViewModel = new AppointmentMedicViewModel()
                    {
                        Medic = viewModel,
                        Appointments = appointments
                    };
                    return View(appointmentViewModel);
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

        [HttpGet]
        public JsonResult GetPatients(int toSkip, Guid idMedic)
        {
            var patients = appointmentService.GetAppointmentsByMedicIdJson(idMedic, toSkip, false);
            var appointmentsViewModel = mapper.Map<IEnumerable<AppointmentsViewModel>>(patients);
            return Json(appointmentsViewModel);
        }

        [HttpGet]
        public IActionResult Profile(Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                var medic = medicService.GetMedicByID(idMedic.ToString());
                if (medic.IdPerson.ToString() == loginViewModel.Id)
                {
                    var specializations = specializationService.GetSpecializations();
                    var medicViewModel = mapper.Map<MedicProfileViewModel>(medic);
                    medicViewModel.Specializations = mapper.Map<IEnumerable<SelectListItem>>(specializations);

                    return View(medicViewModel);
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

        public IActionResult ProfilePut(MedicProfileViewModel medicProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
                {
                    var medic = mapper.Map<Medic>(medicProfileViewModel);
                    var image = GetProfilePicture(medicProfileViewModel);
                    medicService.UpdateMedic(medic, image);
                    return RedirectToAction("Profile", "Medic", new { idMedic = medicProfileViewModel.Medic.Id });
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                var medic = mapper.Map<Medic>(medicProfileViewModel);
                var medicView = mapper.Map<MedicViewModel>(medic);
                medicProfileViewModel.Medic = medicView;
                var specializations = specializationService.GetSpecializations();
                medicProfileViewModel.Specializations = mapper.Map<IEnumerable<SelectListItem>>(specializations);
                return View("Profile", medicProfileViewModel);
            }
        }

        private Image GetProfilePicture(MedicProfileViewModel medicProfileViewModel)
        {
            if (medicProfileViewModel.Image != null)
            {
                var image = new Image();
                using (var memoryStream = new MemoryStream())
                {
                    medicProfileViewModel.Image.CopyToAsync(memoryStream);
                    image._Image = memoryStream.ToArray();

                }
                image.MimeType = medicProfileViewModel.Image.ContentType;
                image.Id = Guid.NewGuid();
                imageService.InsertImage(image);
                return image;
            }
            else
            {
                return null;
            }
        }

        [HttpDelete]
        public IActionResult DeleteProfile(Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (loginViewModel.Id == medicService.GetMedicPersonId(idMedic))
                {
                    medicService.DeleteMedicProfile(idMedic);
                    return RedirectToAction("Logout", "Login");
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

        public IActionResult Appointments(Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                var medic = medicService.GetMedicByID(idMedic.ToString());
                if (loginViewModel.Id == medic.IdPerson.ToString())
                {
                    var appointments = appointmentService.GetAppointmentsByMedicId(idMedic)
                                        .Where(d => d.AppointmentDate >= DateTime.Now);

                    var viewModel = mapper.Map<MedicViewModel>(medic);
                    var appointmentViewModel = new AppointmentMedicViewModel()
                    {
                        Medic = viewModel,
                        Appointments = appointments
                    };

                    return View(appointmentViewModel);
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

        [HttpGet]
        public JsonResult GetAppointments(int toSkip, Guid idMedic)
        {
            var appointments = appointmentService.GetAppointmentsByMedicIdJson(idMedic, toSkip, true);
            var appointmentsViewModel = mapper.Map<IEnumerable<AppointmentsViewModel>>(appointments);
            return Json(appointmentsViewModel);
        }

        [Route("/Medic/DeleteAppointment/{idAppointment}/{idMedic}")]
        public IActionResult DeleteAppointment(Guid idAppointment, Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (loginViewModel.Id == medicService.GetMedicPersonId(idMedic))
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                    var config = builder.Build();
                    appointmentService.sendEmail(idAppointment, config, "delete");
                    appointmentService.DeleteAppointment(idAppointment);
                    return RedirectToAction("Appointments", "Medic", new { idMedic = idMedic });
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

        [Route("Medic/MedicalHistory/{idPatient}")]
        public IActionResult MedicalHistory(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (medicService.IsMedicPatient(idPatient, loginViewModel.Id))
                {
                    var patient = patientService.GetPatientById(idPatient);
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    var viewModel = new HistoryViewModel
                    {
                        Patient = patientViewModel,
                        IdMedic = medicService.GetMedicByPersonID(Guid.Parse(loginViewModel.Id)).Id
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

        [Route("Medic/PatientHistory/{idPatient}")]
        public IActionResult PatientHistory(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (medicService.IsMedicPatient(idPatient, loginViewModel.Id))
                {
                    var patient = patientService.GetPatientById(idPatient);
                    var history = portfolioService.GetPatientPortfolio(idPatient);
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    return View(new MedicalHistoryViewModel
                    {
                        Patient = patientViewModel,
                        Results = history,
                        IdMedic = medicService.GetMedicByPersonID(Guid.Parse(loginViewModel.Id)).Id
                    });
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

        [Route("Medic/PatientMedicine/{idPatient}")]
        public IActionResult PatientMedicine(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (medicService.IsMedicPatient(idPatient, loginViewModel.Id))
                {
                    var patient = patientService.GetPatientById(idPatient);
                    var history = medicineService.GetMedicines(idPatient);
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    return View(new MedicalHistoryViewModel
                    {
                        Patient = patientViewModel,
                        Medicines = history,
                        IdMedic = medicService.GetMedicByPersonID(Guid.Parse(loginViewModel.Id)).Id
                    });
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
        public IActionResult AddMedicine(MedicalHistoryViewModel viewModel)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (medicService.IsMedicPatient(viewModel.IdPatient, loginViewModel.Id))
                {
                    var medicine = new Medicine
                    {
                        Id = Guid.NewGuid(),
                        Name = viewModel.Name,
                        AdministrationMethod = viewModel.Administration
                    };
                    medicineService.InsertMedicine(medicine);
                    medicineService.InsertTreatment(new Treatment
                    {
                        Id = Guid.NewGuid(),
                        IdMedicine = medicine.Id,
                        IdPatient = viewModel.IdPatient,
                        NumberOfDays = viewModel.NumberOfDays,
                        Observations = viewModel.Observations
                    });

                    return RedirectToAction("PatientMedicine", new { idPatient = viewModel.IdPatient });
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
        public async Task<IActionResult> AddPortfolio(MedicalHistoryViewModel viewModel)
        {
            if (viewModel.Document != null)
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
                {
                    if (medicService.IsMedicPatient(viewModel.IdPatient, loginViewModel.Id))
                    {
                        if (viewModel.Document.ContentType.Contains("pdf") || viewModel.Document.ContentType.Contains("image"))
                        {
                            var document = await GetDocument(viewModel);
                            var result = new Result
                            {
                                Id = Guid.NewGuid(),
                                Document = document,
                                MimeType = viewModel.Document.ContentType,
                                Observations = viewModel.Observations,
                                DateOfIssue = DateTime.Now
                            };

                            portfolioService.InsertResult(result);

                            portfolioService.InsertPortfolio(new Portfolio
                            {
                                Id = Guid.NewGuid(),
                                IdResult = result.Id,
                                IdPatient = viewModel.IdPatient,
                                IdMedic = medicService.GetMedicByPersonID(Guid.Parse(loginViewModel.Id)).Id
                            });
                        }
                        else
                        {
                            return View("ErrorPage");
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
            return RedirectToAction("PatientHistory", new { idPatient = viewModel.IdPatient });
        }

        private async Task<byte[]> GetDocument(MedicalHistoryViewModel portfolio)
        {
            byte[] document;
            using (var memoryStream = new MemoryStream())
            {
                await portfolio.Document.CopyToAsync(memoryStream);
                document = memoryStream.ToArray();
            }
            return document;
        }

        public IActionResult DeleteProfilePicture(Guid idImage, Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                var medic = medicService.GetMedicByID(idMedic.ToString());
                if (medic.IdPerson.ToString() == loginViewModel.Id)
                {
                    var image = imageService.GetImageById(idImage);
                    medic.IdImage = null;
                    medic.Image = null;
                    imageService.DeleteImage(image);
                    return RedirectToAction("Profile", new { idMedic = idMedic });
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

        public IActionResult ChangePassword(Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (loginViewModel.Id == medicService.GetMedicPersonId(idMedic))
                {
                    return View(new ChangePasswordViewModel
                    {
                        Medic = medicService.GetMedicByID(idMedic.ToString())
                    });
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
        public IActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
                {
                    if (loginViewModel.Id == medicService.GetMedicPersonId(viewModel.Medic.Id))
                    {
                        var canChangePassword = BCrypt.Net.BCrypt.Verify(viewModel.OldPassword, viewModel.Medic.Person.Password);
                        if (canChangePassword)
                        {
                            var hashPassword = BCrypt.Net.BCrypt.HashPassword(viewModel.NewPassword);
                            medicService.ChangePassword(viewModel.Medic.Id, hashPassword);
                            return RedirectToAction("Profile", new { idMedic = viewModel.Medic.Id });
                        }
                        else
                        {
                            return View(viewModel);
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
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult EditMedicineMedic(Guid idMedicine, Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
            {
                if (loginViewModel.Id == medicService.GetMedicPersonId(idMedic))
                {
                    var medicine = medicineService.GetMedicineById(idMedicine);
                    var treatment = medicineService.GetTreatmentByMedicineId(idMedicine);
                    var viewModel = new TreatmentMedicine
                    {
                        Treatment = treatment,
                        Medicine = medicine
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

        [HttpPost]
        public IActionResult EditMedicineMedic(TreatmentMedicine viewModel)
        {
            if (ModelState.IsValid)
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.FalseString)
                {
                    medicineService.EditMedicine(viewModel.Medicine, viewModel.Treatment);
                    return RedirectToAction("PatientMedicine", "Medic", new { idPatient = viewModel.Treatment.IdPatient });
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        public ActionResult ReadDocument(Guid idDocument)
        {
            var document = portfolioService.GetDocument(idDocument);
            FileResult fileResult = new FileContentResult(document.Document, document.MimeType);
            return fileResult;
        }
    }
}
