using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Patient:IEntity
    {
        public Patient()
        {
            Portfolio = new HashSet<Portfolio>();
            Appointments = new HashSet<Appointment>();
            Treatments = new HashSet<Treatment>();
        }

        public Guid Id { get; set; }
        public Guid IdPerson { get; set; }
        public string Address { get; set; }
        public Guid? IdImage { get; set; }

        public virtual Image Image { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Portfolio> Portfolio { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
