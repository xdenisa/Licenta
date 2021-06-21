using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class PatientViewModel:IValidatableObject
    {
        public Guid Id { get; set; }
        public Guid IdPerson { get; set; }
        public Guid? IdImage { get; set; }

        [Required(ErrorMessage = "Nume obligatoriu!")]
        [DisplayName("Nume")]
        [StringLength(100, ErrorMessage = "Numele trebuie să aibă între 3 și 100 de caractere!",MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Prenume obligatoriu!")]
        [DisplayName("Prenume")]
        [StringLength(100, ErrorMessage = "Prenumele trebuie să aibă între 3 și 100 de caractere!",MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Introduceți adresa!")]
        [DisplayName("Adresă")]
        [StringLength(100, ErrorMessage = "Adresa trebuie să aibă între 5 și 50 de caractere!", MinimumLength = 3)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email obligatoriu!")]
        [StringLength(50, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduceți data nașterii")]
        [DisplayName("Data nașterii")]
        public DateTime? BirthDay { get; set; }

        public string Sex { get; set; }

        [Required(ErrorMessage = "Telefon obligatoriu!")]
        [StringLength(10, ErrorMessage = "Numărul de telefon trebuie sa aibă 10 cifre!", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        public Image Image { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Now.Year - BirthDay.Value.Year < 18)
            {
                yield return new ValidationResult("Trebuie să aveți cel puțin 18 ani!", new List<string> { nameof(BirthDay) });
            }
        }
    }
}
