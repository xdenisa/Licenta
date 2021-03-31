using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Specialization : IEntity
    {
        public Specialization()
        {
            Medics = new HashSet<Medic>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? IdImage { get; set; }

        public virtual ICollection<Medic> Medics { get; set; }
        public virtual Image Image { get; set; }
    }
}
