using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Medicine:IEntity
    {
        public Medicine()
        {
            Treatments = new HashSet<Treatment>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AdministrationMethod { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
