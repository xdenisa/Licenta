using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Person:IEntity
    {
        public Person()
        {
            Medics = new HashSet<Medic>();
            Patients = new HashSet<Patient>();
        }

        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Medic> Medics { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
