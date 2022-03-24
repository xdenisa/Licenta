using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic
{
    public class TreatmentMedicine: IValidatableObject
    {
        public Medicine Medicine { get; set; }
        public Treatment Treatment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(Medicine.Name) || Medicine.Name.Length>100 || Medicine.Name.Length<2 )
            {
                yield return new ValidationResult("", new List<string> { nameof(Medicine.Name) });
            }

            if (String.IsNullOrEmpty(Medicine.AdministrationMethod) || Medicine.AdministrationMethod.Length > 100 || Medicine.AdministrationMethod.Length<2)
            {
                yield return new ValidationResult("", new List<string> { nameof(Medicine.AdministrationMethod) });
            }

            if(Treatment.NumberOfDays<1 || Treatment.NumberOfDays>365)
            {
                yield return new ValidationResult("", new List<string> { nameof(Treatment.NumberOfDays) });
            }
        }
    }
}
