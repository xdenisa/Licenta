using Proiect.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models.Account
{
    public class RegisterViewModel:IValidatableObject
    {
        [Required(ErrorMessage ="Email obligatoriu!")]
        [StringLength(50,MinimumLength =5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Parolă obligatorie!")]
        [DisplayName("Parolă")]
        [StringLength(50,MinimumLength =5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nume obligatoriu!")]
        [DisplayName("Nume")]
        [StringLength(100,MinimumLength =3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Prenume obligatoriu!")]
        [DisplayName("Prenume")]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Introduceți data nașterii")]
        [DisplayName("Data nașterii")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Sex obligatoriu!")]
        [DisplayName("Sex")]
        public Gender Sex { get; set; }

        [Required(ErrorMessage = "Introduceți numărul de telefon!")]
        [DisplayName("Telefon")]
        [StringLength(10,MinimumLength =10)]
        public string PhoneNumber { get; set; }

        public string IsPacient { get; set; }

        public RegisterViewModel()
        {
            
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateTime.Now.Year - BirthDay.Year <18)
            {
                yield return new ValidationResult("Trebuie sa aveti cel putin 18 ani!", new List<string> { nameof(BirthDay) });
            }
            if (BirthDay.Year <DateTime.Now.Year-120)
            {
                yield return new ValidationResult("Anul nasterii este invalid", new List<string> { nameof(BirthDay) });
            }
        }
    }
}
