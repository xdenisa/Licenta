using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class AppointmentMedicViewModel
    { 
        public IEnumerable<Appointment> Appointments { get; set; }
        public Medic Medic { get; set; }

    }
}
