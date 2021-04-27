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
    public class MedicProfileViewModel
    {
        public IFormFile Image { get; set; }
        public MedicViewModel Medic { get; set; }
        public IEnumerable<SelectListItem> Specializations { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    //if (Medic.Email == null || Medic.Email.Length < 5 || Medic.Email.Length > 50)
        //    //{
        //    //    yield return new ValidationResult("Emailul trebuie sa aiba intre 5 si 50 de caractere!", new List<string> { nameof(Medic.Email) });
        //    //}

        //    //if (Medic.LastName == null || Medic.LastName.Length < 3 || Medic.LastName.Length > 100)
        //    //{
        //    //    yield return new ValidationResult("Numele trebuie sa aiba intre 3 si 100 de caractere!", new List<string> { nameof(Medic.LastName) });
        //    //}

        //    //if (Medic.FirstName == null || Medic.FirstName.Length < 3 || Medic.FirstName.Length > 100)
        //    //{
        //    //    yield return new ValidationResult("Prenumele trebuie sa aiba intre 3 si 100 de caractere!", new List<string> { nameof(Medic.FirstName) });
        //    //}

        //    //if (Medic.PhoneNumber == null || Medic.PhoneNumber.Length != 10)
        //    //{
        //    //    yield return new ValidationResult("Numarul de telefon trebuie sa aiba 10 caractere!", new List<string> { nameof(Medic.PhoneNumber) });
        //    //}

        //    //if (Medic.MedicalCollegeYear == null || Medic.MedicalCollegeYear.ToString().Length != 4)
        //    //{
        //    //    yield return new ValidationResult("Anul inscrierii in colegiul medicilor este obligatoriu!", new List<string> { nameof(Medic.MedicalCollegeYear) });
        //    //}

        //    //if (Medic.BirthDay > DateTime.Today || DateTime.Now.Year - Medic.BirthDay.Value.Year < 18)
        //    //{
        //    //    yield return new ValidationResult("Data nasterii invalida!", new List<string> { nameof(Medic.BirthDay) });
        //    //}

        //}
    }
}
