using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Image:IEntity
    {
        public Image()
        {
            Medics = new HashSet<Medic>();
            Patients = new HashSet<Patient>();
            Results = new HashSet<Result>();
        }

        public Guid Id { get; set; }
        public byte[] _Image { get; set; }
        public string Description { get; set; }
        public string MimeType { get; set; }

        public virtual ICollection<Medic> Medics { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
