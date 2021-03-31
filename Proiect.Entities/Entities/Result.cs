using Proiect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.Entities
{
    public class Result:IEntity
    {
        public Result()
        {
            Portfolio = new HashSet<Portfolio>();
        }

        public Guid Id { get; set; }
        public string Observations { get; set; }
        public byte[] Document { get; set; }
        public Guid? IdImage { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string MimeType { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<Portfolio> Portfolio { get; set; }
    }
}
