using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Appointment:IEntity
    {
        public Guid Id { get; set; }
        public Guid IdPatient { get; set; }
        public Guid IdMedic { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Details { get; set; }
        public string Type { get; set; }

        public virtual Medic Medic { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
