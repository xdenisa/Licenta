using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class AppointmentPatientViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public PatientViewModel Patient { get; set; }
    }
}
