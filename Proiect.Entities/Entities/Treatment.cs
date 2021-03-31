using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
   public class Treatment:IEntity
    {
        public Guid Id { get; set; }
        public Guid IdPatient { get; set; }
        public Guid IdMedicine { get; set; }
        public int NumberOfDays { get; set; }
        public string Observations { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
