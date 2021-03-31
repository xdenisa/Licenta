using Proiect.BusinessLogic;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicineViewModel : IValidatableObject
    {
        public IEnumerable<TreatmentMedicine> Medicines { get; set; }
        public PatientViewModel Patient { get; set; }
        public string Name { get; set; }
        public string Administration { get; set; }
        public int NumberOfDays { get; set; }
        public string Observations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == null)
            {
                yield return new ValidationResult("Campul trebuie completat!", new List<string> { nameof(Name) });
            }
            if (Administration == null)
            {
                yield return new ValidationResult("Campul trebuie completat!", new List<string> { nameof(Administration) });
            }
            if (NumberOfDays <1)
            {
                yield return new ValidationResult("Campul trebuie completat!", new List<string> { nameof(NumberOfDays) });
            }
        }
    }
}
