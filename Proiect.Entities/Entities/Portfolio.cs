using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Portfolio:IEntity
    {
        public Guid Id { get; set; }
        public Guid IdPatient { get; set; }
        public Guid? IdMedic { get; set; }
        public Guid IdResult { get; set; }

        public virtual Medic Medic { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Result Results { get; set; }
    }
}
