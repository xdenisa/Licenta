using Proiect.BusinessLogic;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MakeAppointmentViewModel : IValidatableObject
    {
        public PatientViewModel Patient { get; set; }
        public Medic Medic { get; set; }
        public Appointment Appointment { get; set; }
        public bool IsEdit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var appService = validationContext.GetService(typeof(AppointmentService)) as AppointmentService;
            if (!IsEdit && (Appointment.AppointmentDate <= DateTime.Today || appService.CanMakeAppointment(Appointment) == false))
            {
                yield return new ValidationResult("Data programarii este invalida!", new List<string> { nameof(Appointment.AppointmentDate) });
            }
            if (IsEdit && (Appointment.AppointmentDate <= DateTime.Today || appService.CanEditAppointment(Appointment)==false))
            {
                yield return new ValidationResult("Data programarii este invalida!", new List<string> { nameof(Appointment.AppointmentDate) });
            }
        }
    }
}
