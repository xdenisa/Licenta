using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class AppointmentsViewModel
    {
        public string MedicFirstName { get; set; }
        public string MedicLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string Details { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Type { get; set; }
        public Guid IdAppointment { get; set; }
        public Guid IdPatient { get; set; }
        public Guid IdMedic { get; set; }
    }
}
