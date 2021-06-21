using AutoMapper;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Proiect.BusinessLogic;
using Proiect.DataAccess.EntityFramework;
using Proiect.Entities;
using Proiect.WebApp.Models;
using Proiect.WebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Proiect.WebApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly LoginViewModel loginViewModel;
        private readonly SpecializationService specializationService;
        private readonly MedicService medicService;
        private readonly PatientService patientService;
        private readonly AppointmentService appointmentService;
        private readonly ImageService imageService;
        private readonly PortfolioService portfolioService;
        private readonly MedicineService medicineService;
        private readonly IMapper mapper;
        public PatientController(LoginViewModel loginViewModel,
            SpecializationService specializationService,
            MedicService medicService,
            PatientService patientService,
            AppointmentService appointmentService,
            ImageService imageService,
            PortfolioService portfolioService,
            MedicineService medicineService,
            IMapper mapper)
        {
            this.specializationService = specializationService;
            this.medicService = medicService;
            this.patientService = patientService;
            this.appointmentService = appointmentService;
            this.imageService = imageService;
            this.portfolioService = portfolioService;
            this.medicineService = medicineService;
            this.loginViewModel = loginViewModel;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(Guid idPerson)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString && loginViewModel.Id == idPerson.ToString())
            {
                var patient = patientService.GetPatientByPersonId(idPerson);
                var patientViewModel = mapper.Map<PatientViewModel>(patient);
                return View(patientViewModel);
            }
            else
            {
                return View("NoAccessPage");
            }
        }

        [HttpGet]
        public IActionResult Profile(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (loginViewModel.Id == patient.IdPerson.ToString())
                {
                    var profileViewModel = mapper.Map<PatientProfileViewModel>(patient);
                    return View(profileViewModel);
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

        public IActionResult ProfilePut(PatientProfileViewModel patientProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
                {
                    var patient = mapper.Map<Patient>(patientProfileViewModel);
                    var image = GetProfilePicture(patientProfileViewModel);
                    patientService.UpdatePatient(patient, image);
                    return RedirectToAction("Profile", new { idPatient = patientProfileViewModel.Patient.Id });
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                var patient = mapper.Map<Patient>(patientProfileViewModel);
                var patientView = mapper.Map<PatientViewModel>(patient);
                patientProfileViewModel.Patient = patientView;
                return View("Profile", patientProfileViewModel);
            }
        }

        private Image GetProfilePicture(PatientProfileViewModel patientProfileViewModel)
        {
            if (patientProfileViewModel.Image != null)
            {
                var image = new Image();
                using (var memoryStream = new MemoryStream())
                {
                    patientProfileViewModel.Image.CopyToAsync(memoryStream);
                    image._Image = memoryStream.ToArray();

                }
                image.MimeType = patientProfileViewModel.Image.ContentType;
                image.Id = Guid.NewGuid();
                imageService.InsertImage(image);
                return image;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public IActionResult Portfolio(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (loginViewModel.Id == patient.IdPerson.ToString())
                {
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    var portfolio = portfolioService.GetPatientPortfolio(idPatient);
                    var portfolioViewModel = new PortfolioViewModel
                    {
                        Patient = patientViewModel,
                        Portfolio = portfolio
                    };
                    return View(portfolioViewModel);
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

        private async Task<byte[]> GetDocument(PortfolioViewModel portfolio)
        {
            byte[] document;
            using (var memoryStream = new MemoryStream())
            {
                await portfolio.Document.CopyToAsync(memoryStream);
                document = memoryStream.ToArray();
            }
            return document;
        }

        [HttpPost]
        public async Task<IActionResult> Portfolio(PortfolioViewModel portfolioViewModel)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (portfolioViewModel.Document != null)
                {
                    if (portfolioViewModel.Document.ContentType.Contains("pdf") || portfolioViewModel.Document.ContentType.Contains("image"))
                    {
                        var document = await GetDocument(portfolioViewModel);
                        var result = new Result
                        {
                            Id = Guid.NewGuid(),
                            Document = document,
                            MimeType = portfolioViewModel.Document.ContentType,
                            Observations = portfolioViewModel.Observations,
                            DateOfIssue = DateTime.Now
                        };

                        portfolioService.InsertResult(result);

                        portfolioService.InsertPortfolio(new Portfolio
                        {
                            Id = Guid.NewGuid(),
                            IdResult = result.Id,
                            IdPatient = portfolioViewModel.Patient.Id
                        });
                    }
                    else
                    {
                        return View("ErrorPage");
                    }
                }
            }
            else
            {
                return View("NoAccessPage");
            }

            return RedirectToAction("Portfolio", new { idPatient = portfolioViewModel.Patient.Id });
        }

        [HttpGet]
        public IActionResult Medicines(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (loginViewModel.Id == patient.IdPerson.ToString())
                {
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    return View(new MedicineViewModel
                    {
                        Patient = patientViewModel,
                        Medicines = medicineService.GetMedicines(idPatient)
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

        [HttpGet]
        public IActionResult MedicCard(string specializationName, Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (loginViewModel.Id == patient.IdPerson.ToString())
                {
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    var medics = medicService.GetMedicsBySpecialization(specializationName);
                    var viewmodel = new MedicsForPatientsViewModel()
                    {
                        Medics = medics,
                        Patient = patientViewModel
                    };
                    return View(viewmodel);
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
        public IActionResult Medics(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (loginViewModel.Id == patient.IdPerson.ToString())
                {
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    var specializations = specializationService.GetSpecializationsWithImage();

                    var viewmodel = new MedicsForPatientsViewModel()
                    {
                        Patient = patientViewModel,
                        Specializations = specializations
                    };
                    return View(viewmodel);
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
        public IActionResult Appointments(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (loginViewModel.Id == patient.IdPerson.ToString())
                {
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    var appointments = appointmentService.GetAppointmentsByPatientId(idPatient);
                    var appointmentViewModel = new AppointmentPatientViewModel()
                    {
                        Patient = patientViewModel,
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
        public JsonResult GetAppointments(int toSkip, Guid idPatient)
        {
            var appointments = appointmentService.GetAppointmentsByPatientIdJson(idPatient, toSkip);
            var appointmentsViewModel = mapper.Map<IEnumerable<AppointmentsViewModel>>(appointments);
            return Json(appointmentsViewModel);
        }

        [HttpGet]
        public IActionResult MakeAppointment(Guid idPatient, Guid idMedic)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (loginViewModel.Id == patient.IdPerson.ToString())
                {
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    var medic = medicService.GetMedicByID(idMedic.ToString());
                    medic.Person.Password = null;
                    var viewModel = new MakeAppointmentViewModel()
                    {
                        Patient = patientViewModel,
                        Medic = medic,
                        isValid=true

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
        public IActionResult MakeAppointment(MakeAppointmentViewModel viewModel)
        {
            if (appointmentService.CanMakeAppointment(viewModel.Appointment,viewModel.Patient.Id,viewModel.Medic.Id))
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                    var config = builder.Build();

                    
                    appointmentService.MakeAppointment(viewModel.Medic.Id, viewModel.Patient.Id, viewModel.Appointment);
                    appointmentService.sendEmail(viewModel.Appointment.Id, config, "new");
                    return RedirectToAction("Appointments", "Patient", new { idPatient = viewModel.Patient.Id });
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                viewModel.isValid = false;
                return View(viewModel);
            }
        }

        [Route("/Patient/DeleteAppointment/{idAppointment}/{idPatient}")]
        public IActionResult DeleteAppointment(Guid idAppointment, Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (loginViewModel.Id == patientService.GetPatientPersonId(idPatient))
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                    var config = builder.Build();

                    appointmentService.sendEmail(idAppointment, config,"delete");
                    appointmentService.DeleteAppointment(idAppointment);

                    return RedirectToAction("Appointments", "Patient", new { idPatient = idPatient });
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

        [HttpDelete]
        public IActionResult DeleteMedicine(Guid idTreatment, Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (loginViewModel.Id == patientService.GetPatientPersonId(idPatient))
                {
                    medicineService.DeleteMedicine(idTreatment);
                    return RedirectToAction("Medicines", new { idPatient = idPatient });
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


        [HttpDelete]
        public IActionResult DeletePortfolio(Guid idResult, Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (loginViewModel.Id == patientService.GetPatientPersonId(idPatient))
                {
                    portfolioService.DeletePortfolio(idResult);
                    return RedirectToAction("Portfolio", new { idPatient = idPatient });
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

        [HttpDelete]
        public IActionResult DeleteProfile(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (loginViewModel.Id == patientService.GetPatientPersonId(idPatient))
                {
                    patientService.DeletePatientProfile(idPatient);
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

        [HttpGet]
        [Route("Patient/AddMedicine/{idPatient}")]
        public IActionResult AddMedicine(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (loginViewModel.Id == patientService.GetPatientPersonId(idPatient))
                {
                    var patient = patientService.GetPatientById(idPatient);
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    return View(new MedicineViewModel
                    {
                        Patient = patientViewModel
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
        public IActionResult AddMedicine(MedicineViewModel medicineViewModel)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (String.IsNullOrEmpty(medicineViewModel.Name) ||
                    String.IsNullOrEmpty(medicineViewModel.Administration) || 
                    medicineViewModel.NumberOfDays<1)
                {
                    return View(medicineViewModel);
                }

                var medicine = new Medicine
                {
                    Id = Guid.NewGuid(),
                    Name = medicineViewModel.Name,
                    AdministrationMethod = medicineViewModel.Administration
                };
                medicineService.InsertMedicine(medicine);
                medicineService.InsertTreatment(new Treatment
                {
                    Id = Guid.NewGuid(),
                    IdMedicine = medicine.Id,
                    IdPatient = medicineViewModel.Patient.Id,
                    NumberOfDays = medicineViewModel.NumberOfDays,
                    Observations = medicineViewModel.Observations
                });
                return RedirectToAction("Medicines", new { idPatient = medicineViewModel.Patient.Id });
            }
            else
            {
                return View("NoAccessPage");
            }
            
        }

        public IActionResult DeleteProfilePicture(Guid idImage, Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var patient = patientService.GetPatientById(idPatient);
                if (patient.IdPerson.ToString() == loginViewModel.Id)
                {
                    var image = imageService.GetImageById(idImage);
                    patient.IdImage = null;
                    patient.Image = null;
                    imageService.DeleteImage(image);
                    return RedirectToAction("Profile", new { idPatient = idPatient });
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
        public IActionResult ChangePassword(Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (loginViewModel.Id == patientService.GetPatientPersonId(idPatient))
                {
                    return View(new ChangePasswordViewModel
                    {
                        Patient = patientService.GetPatientById(idPatient)
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
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
                {
                    if (loginViewModel.Id == patientService.GetPatientPersonId(viewModel.Patient.Id))
                    {
                        var canChangePassword = BCrypt.Net.BCrypt.Verify(viewModel.OldPassword, viewModel.Patient.Person.Password);
                        if (canChangePassword)
                        {
                            var hashPassword = BCrypt.Net.BCrypt.HashPassword(viewModel.NewPassword);
                            patientService.ChangePassword(viewModel.Patient.Id, hashPassword);
                            return RedirectToAction("Profile", new { idPatient = viewModel.Patient.Id });
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
        [Route("/Patient/EditAppointment/{idAppointment}")]
        public IActionResult EditAppointment(Guid idAppointment)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                var appointment = appointmentService.GetAppointmentById(idAppointment);
                var patient = patientService.GetPatientById(appointment.IdPatient);
                if (loginViewModel.Id == patient.Person.Id.ToString())
                {
                    var patientViewModel = mapper.Map<PatientViewModel>(patient);
                    var medic = medicService.GetMedicByID(appointment.IdMedic.ToString());
                    var viewModel = new MakeAppointmentViewModel
                    {
                        Patient = patientViewModel,
                        Medic = medic,
                        Appointment = appointment,
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
        public IActionResult EditAppointment(MakeAppointmentViewModel viewModel)
        {
            if (appointmentService.CanEditAppointment(viewModel.Appointment, viewModel.Patient.Id, viewModel.Medic.Id))
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                    var config = builder.Build();

                    appointmentService.sendEmail(viewModel.Appointment.Id, config,"edit");
                    appointmentService.EditAppointment(viewModel.Appointment);
                    return RedirectToAction("Appointments", new { idPatient = viewModel.Patient.Id });
                }
                else
                {
                    return View("NoAccessPage");
                }
            }
            else
            {
                viewModel.isValid = false;
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult EditMedicine(Guid idMedicine, Guid idPatient)
        {
            if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
            {
                if (loginViewModel.Id == patientService.GetPatientPersonId(idPatient))
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
        public IActionResult EditMedicine(TreatmentMedicine viewModel)
        {
            if (ModelState.IsValid)
            {
                if (loginViewModel.IsLogedIn && loginViewModel.IsPacient == bool.TrueString)
                {
                    medicineService.EditMedicine(viewModel.Medicine, viewModel.Treatment);
                    return RedirectToAction("Medicines", new { idPatient = viewModel.Treatment.IdPatient });
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
