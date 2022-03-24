using Microsoft.AspNetCore.Http;
using Proiect.BusinessLogic;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicalHistoryViewModel
    {
        public PatientViewModel Patient { get; set; }
        public IEnumerable<TreatmentMedicine> Medicines { get; set; }
        public IEnumerable<Result> Results { get; set; }
        public IFormFile Document { get; set; }
        public string Observations { get; set; }
        public Guid IdPatient { get; set; }
        public string Name { get; set; }
        public string Administration { get; set; }
        public int NumberOfDays { get; set; }
        public Guid IdMedic { get; set; }

    }
}
