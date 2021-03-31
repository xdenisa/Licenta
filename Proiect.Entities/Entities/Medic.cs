using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Medic:IEntity
    {
        public Medic()
        {
            Portfolio = new HashSet<Portfolio>();
            Appointments = new HashSet<Appointment>();
        }

        public Guid Id { get; set; }
        public Guid IdPerson { get; set; }
        public int? IdentificationCode { get; set; }
        public int? MedicalCollegeYear { get; set; }
        public Guid? IdSpecialization { get; set; }
        public string Hospital { get; set; }
        public string Abilities { get; set; }
        public string Education { get; set; }
        public string Description { get; set; }
        public Guid? IdImage { get; set; }
        public string IsApproved { get; set; }

        public virtual Image Image { get; set; }
        public virtual Person Person { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Portfolio> Portfolio { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
