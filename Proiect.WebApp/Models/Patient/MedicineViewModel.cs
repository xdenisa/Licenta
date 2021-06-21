using Proiect.BusinessLogic;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicineViewModel
    {
        public IEnumerable<TreatmentMedicine> Medicines { get; set; }
        public PatientViewModel Patient { get; set; }

        [Required(ErrorMessage = "Introduceți numele medicamentului!")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Introduceți metoda de administrare!")]
        [StringLength(100, MinimumLength = 2)]
        public string Administration { get; set; }

        [Required(ErrorMessage = "Introduceți numărul de zile de tratament!")]
        [Range(1, 365, ErrorMessage = "Numărul de zile de tratament trebuie să fie între 1 și 365 de zile!")]
        public int NumberOfDays { get; set; }
        public string Observations { get; set; }
    }
}
