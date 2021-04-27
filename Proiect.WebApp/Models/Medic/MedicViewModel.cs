using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicViewModel
    {
        public Guid Id { get; set; }
        public Guid IdPerson { get; set; }
        public Guid? IdImage { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public Image Image { get; set; }
        public int? IdentificationCode { get; set; }
        public int? MedicalCollegeYear { get; set; }
        public Guid? IdSpecialization { get; set; }
        public string Hospital { get; set; }
        public string Abilities { get; set; }
        public string Education { get; set; }
        public string Description { get; set; }
        public string IsApproved { get; set; }
        public Specialization Specialization { get; set; }
    }
}
