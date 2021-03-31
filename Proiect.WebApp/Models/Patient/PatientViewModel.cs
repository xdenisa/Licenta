using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class PatientViewModel
    {
        public Guid Id { get; set; }
        public Guid IdPerson { get; set; }
        public Guid? IdImage { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public Image Image { get; set; }

    }
}
