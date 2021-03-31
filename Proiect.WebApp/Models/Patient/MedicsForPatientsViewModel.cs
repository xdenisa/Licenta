using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicsForPatientsViewModel
    {
        public PatientViewModel Patient { get; set; }
        public IEnumerable<Specialization> Specializations { get; set; }
        public IEnumerable<Medic> Medics { get; set; }
    }
}
