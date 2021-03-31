using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicProfileViewModel:IValidatableObject
    {
        public IFormFile Image { get; set; }
        public MedicViewModel Medic { get; set; }
        public IEnumerable<SelectListItem> Specializations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Medic.Person.Email == null || Medic.Person.Email.Length < 5 || Medic.Person.Email.Length > 50)
            {
                yield return new ValidationResult("Emailul trebuie sa aiba intre 5 si 50 de caractere!", new List<string> { nameof(Medic.Person.Email) });
            }

            if (Medic.Person.LastName == null || Medic.Person.LastName.Length < 3 || Medic.Person.LastName.Length > 100)
            {
                yield return new ValidationResult("Numele trebuie sa aiba intre 3 si 100 de caractere!", new List<string> { nameof(Medic.Person.LastName) });
            }

            if (Medic.Person.FirstName == null || Medic.Person.FirstName.Length < 3 || Medic.Person.FirstName.Length > 100)
            {
                yield return new ValidationResult("Prenumele trebuie sa aiba intre 3 si 100 de caractere!", new List<string> { nameof(Medic.Person.FirstName) });
            }

            if (Medic.Person.PhoneNumber == null || Medic.Person.PhoneNumber.Length != 10)
            {
                yield return new ValidationResult("Numarul de telefon trebuie sa aiba 10 caractere!", new List<string> { nameof(Medic.Person.PhoneNumber) });
            }

            if (Medic.MedicalCollegeYear == null || Medic.MedicalCollegeYear.ToString().Length != 4)
            {
                yield return new ValidationResult("Anul inscrierii in colegiul medicilor este obligatoriu!", new List<string> { nameof(Medic.MedicalCollegeYear) });
            }

            if (Medic.Person.BirthDay > DateTime.Today || DateTime.Now.Year - Medic.Person.BirthDay.Value.Year < 18)
            {
                yield return new ValidationResult("Data nasterii invalida!", new List<string> { nameof(Medic.Person.BirthDay) });
            }

        }
    }
}
