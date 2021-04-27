using Microsoft.EntityFrameworkCore;
using Proiect.BusinessLogic.Base;
using Proiect.DataAccess;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Proiect.BusinessLogic
{
    public class AppointmentService : BaseService
    {
        private PatientService patientService;
        public AppointmentService(UnitOfWork unitOfWork,PatientService patientService) : base(unitOfWork)
        {
            this.patientService = patientService;
        }

        public IEnumerable<Appointment> GetAppointmentsByMedicId(Guid idMedic)
        {
            return unitOfWork.Appointments
                .Get()
                .Where(i => i.IdMedic == idMedic)
                .Include(p => p.Patient)
                .ThenInclude(p => p.Person)
                .Include(m => m.Medic)
                .ThenInclude(p => p.Person)
                .OrderByDescending(x => x.AppointmentDate)
                .ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByPatientId(Guid idPatient)
        {
            return unitOfWork.Appointments.Get()
                .Where(i => i.IdPatient == idPatient)
                .Include(p => p.Patient)
                .ThenInclude(p => p.Person)
                .Include(m => m.Medic)
                .ThenInclude(p => p.Person)
                .OrderByDescending(x => x.AppointmentDate)
                .ToList();
        }

        public Appointment GetAppointmentById(Guid idAppointment)
        {
            return unitOfWork.Appointments.Get()
                .Include(p => p.Patient)
                .ThenInclude(p => p.Person)
                .Include(m => m.Medic)
                .ThenInclude(p => p.Person)
                .FirstOrDefault(i => i.Id == idAppointment);
        }

        public void DeleteAppointment(Guid idAppointment)
        {
            var appointmentToDelete = GetAppointmentById(idAppointment);
            unitOfWork.Appointments.Delete(appointmentToDelete);
            unitOfWork.SaveChanges();
        }

        public void MakeAppointment(Guid idMedic, Guid idPatient, Appointment appointment)
        {
            appointment.IdMedic = idMedic;
            appointment.IdPatient = idPatient;
            unitOfWork.Appointments.Insert(appointment);
            unitOfWork.SaveChanges();
        }

        public IEnumerable<Appointment> GetAppointmentsByMedicIdJson(Guid idMedic, int toSkip, bool active)
        {
            IEnumerable<Appointment> patients;
            if (active)
            {
                patients = unitOfWork.Appointments
                 .Get()
                 .Where(i => i.IdMedic == idMedic && i.AppointmentDate >= DateTime.Now)
                 .Skip(toSkip)
                 .Take(5)
                 .Include(p => p.Patient)
                 .ThenInclude(p => p.Person)
                 .Include(m => m.Medic)
                 .ThenInclude(p => p.Person)
                 .OrderByDescending(x => x.AppointmentDate)
                 .ToList();
            }
            else
            {
                patients = unitOfWork.Appointments
                 .Get()
                 .Where(i => i.IdMedic == idMedic && i.AppointmentDate < DateTime.Now)
                 .Skip(toSkip)
                 .Take(5)
                 .Include(p => p.Patient)
                 .ThenInclude(p => p.Person)
                 .Include(m => m.Medic)
                 .ThenInclude(p => p.Person)
                 .OrderByDescending(x => x.AppointmentDate)
                 .ToList();
            }

            foreach (var patient in patients)
            {
                patient.Patient.Appointments = null;
                patient.Medic.Appointments = null;
                patient.Medic.Person.Medics = null;
                patient.Patient.Person.Patients = null;
                patient.Patient.Person.Medics = null;
                patient.Medic.Person.Patients = null;
                patient.Patient.Person.Password = null;
                patient.Medic.Person.Password = null;
            }

            return patients;
        }

        public IEnumerable<Appointment> GetAppointmentsByPatientIdJson(Guid idPatient, int toSkip)
        {
            var appointments = unitOfWork.Appointments
                .Get()
                .Where(i => i.IdPatient == idPatient)
                .Skip(toSkip)
                .Take(5)
                .Include(p => p.Patient)
                .ThenInclude(p => p.Person)
                .Include(m => m.Medic)
                .ThenInclude(p => p.Person)
                .OrderByDescending(x => x.AppointmentDate)
                .ToList();

            foreach (var appointment in appointments)
            {
                appointment.Medic.Appointments = null;
                appointment.Patient.Appointments = null;
                appointment.Medic.Person.Medics = null;
                appointment.Medic.Person.Patients = null;
                appointment.Patient.Person.Patients = null;
                appointment.Patient.Person.Medics = null;
                appointment.Patient.Person.Password = null;
                appointment.Medic.Person.Password = null;
            }

            return appointments;
        }

        public bool CanMakeAppointment(Appointment appointment)
        {
            try
            {
                var appointment2 = unitOfWork.Appointments.Get()
                .First(a => a.AppointmentDate.Date == appointment.AppointmentDate.Date
                && a.AppointmentDate.Hour == appointment.AppointmentDate.Hour);
                if (appointment2.IdMedic == appointment.IdMedic && appointment2.IdPatient == appointment.IdPatient)
                {
                    return false;
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public bool CanEditAppointment(Appointment appointment)
        {
            try
            {
                var appointment2 = unitOfWork.Appointments.Get()
                    .First(a => a.AppointmentDate == appointment.AppointmentDate
                    && a.IdMedic == appointment.IdMedic);
                return false;

            }
            catch
            {
                return true;
            }
        }

        public void EditAppointment(Appointment appointment)
        {
            unitOfWork.Appointments.Update(appointment);
            unitOfWork.SaveChanges();
        }

        public void sendEmail(Guid idAppointment,IConfigurationRoot config)
        {
            var emailDetails =  patientService.getPatientEmailByAppointment(idAppointment);

            var message = new MailMessage();
            message.From = new MailAddress(config["Username"]);
            message.To.Add((string)(emailDetails?.GetType().GetProperty("Email").GetValue(emailDetails)));
            message.Subject = "Programarea a fost anulată!";
            message.Body = "Dorim să vă informăm că programarea din data de "
                + emailDetails?.GetType().GetProperty("AppointmentDate").GetValue(emailDetails)
                + " la medicul " + emailDetails?.GetType().GetProperty("Medic").GetValue(emailDetails)
                + " a fost anulată!";

            var smtp = new SmtpClient
            {
                Host = config["Host"],
                Port = int.Parse(config["Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(config["Username"], config["Password"])
            };

            smtp.Send(message);

            message.To.RemoveAt(0);
            message.To.Add((string)(emailDetails?.GetType().GetProperty("MedicEmail").GetValue(emailDetails)));
            message.Subject = "Programarea a fost anulată!";
            message.Body = "Dorim să vă informăm că programarea din data de "
                + emailDetails?.GetType().GetProperty("AppointmentDate").GetValue(emailDetails)
                + " a pacientului " + emailDetails?.GetType().GetProperty("Patient").GetValue(emailDetails)
                + " a fost anulată!";

            smtp.Send(message);
            smtp.Dispose();
        }
    }
}
