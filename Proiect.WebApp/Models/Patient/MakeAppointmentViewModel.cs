using Proiect.BusinessLogic;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MakeAppointmentViewModel
    {
        public PatientViewModel Patient { get; set; }
        public Medic Medic { get; set; }
        public Appointment Appointment { get; set; }
        public bool isValid { get; set; }

    }
}
