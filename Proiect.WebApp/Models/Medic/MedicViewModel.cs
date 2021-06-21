using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicViewModel:IValidatableObject
    {
        public Guid Id { get; set; }
        public Guid IdPerson { get; set; }
        public Guid? IdImage { get; set; }


        [Required(ErrorMessage = "Nume obligatoriu!")]
        [DisplayName("Nume")]
        [StringLength(100, ErrorMessage = "Numele trebuie să aibă între 3 și 100 de caractere!", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Prenume obligatoriu!")]
        [DisplayName("Prenume")]
        [StringLength(100, ErrorMessage = "Prenumele trebuie să aibă între 3 și 100 de caractere!", MinimumLength = 3)]
        public string FirstName { get; set; }

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

        [Required(ErrorMessage = "Cod obligatoriu!")]
        public int? IdentificationCode { get; set; }

        [Required(ErrorMessage = "Introduceți anul intrării în Colegiul Medicilor!")]
        public int? MedicalCollegeYear { get; set; }
        public Guid? IdSpecialization { get; set; }

        [StringLength(100, ErrorMessage = "Introduceți spitalul la care lucrați!")]
        public string Hospital { get; set; }
        public string Abilities { get; set; }
        public string Education { get; set; }
        public string Description { get; set; }
        public string IsApproved { get; set; }
        public Specialization Specialization { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Now.Year - BirthDay.Value.Year < 18)
            {
                yield return new ValidationResult("Trebuie să aveți cel puțin 18 ani!", new List<string> { nameof(BirthDay) });
            }

            if (IdentificationCode.ToString().Length != 10)
            {
                yield return new ValidationResult("Codul Unic de Identificare trebuie sa aibă 10 cifre!", new List<string> { nameof(IdentificationCode) });
            }

            if (MedicalCollegeYear<DateTime.Now.Year-70 || MedicalCollegeYear>DateTime.Now.Year)
            {
                yield return new ValidationResult("Anul nu este valid!", new List<string> { nameof(MedicalCollegeYear) });
            }
        }
    }
}
