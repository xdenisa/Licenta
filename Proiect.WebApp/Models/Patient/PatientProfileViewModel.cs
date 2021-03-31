using Microsoft.AspNetCore.Http;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class PatientProfileViewModel : IValidatableObject
    {
        public IFormFile Image { get; set; }
        public PatientViewModel Patient { get; set; }     
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Patient.Email == null || Patient.Email.Length < 5 || Patient.Email.Length > 50)
            {
                yield return new ValidationResult("Emailul trebuie sa aiba intre 5 si 50 de caractere!", new List<string> { nameof(Patient.Email) });
            }

            if (Patient.LastName == null || Patient.LastName.Length < 3 || Patient.LastName.Length > 100)
            {
                yield return new ValidationResult("Numele trebuie sa aiba intre 3 si 100 de caractere!", new List<string> { nameof(Patient.LastName) });
            }

            if (Patient.FirstName == null || Patient.FirstName.Length < 3 || Patient.FirstName.Length > 100)
            {
                yield return new ValidationResult("Prenumele trebuie sa aiba intre 3 si 100 de caractere!", new List<string> { nameof(Patient.FirstName) });
            }

            if (Patient.PhoneNumber == null || Patient.PhoneNumber.Length !=10)
            {
                yield return new ValidationResult("Numarul de telefon trebuie sa aiba 10 caractere!", new List<string> { nameof(Patient.PhoneNumber) });
            }

            if (Patient.BirthDay > DateTime.Today || DateTime.Now.Year - Patient.BirthDay.Value.Year < 18)
            {
                yield return new ValidationResult("Data nasterii invalida!", new List<string> { nameof(Patient.BirthDay) });
            }

        }
    }
}
