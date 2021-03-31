using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class ChangePasswordViewModel:IValidatableObject
    {
        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 5)]
        public string OldPassword { get; set; }
        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 5)]
        public string NewPassword { get; set; }
        public Patient Patient { get; set; }
        public Medic Medic { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OldPassword==NewPassword)
            {
                yield return new ValidationResult("Noua parola nu poate fi vechea parola!", new List<string> { nameof(OldPassword) });
            }
        }
    }
}
