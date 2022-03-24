using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{ 
    public class HistoryViewModel
    {
        public PatientViewModel Patient { get; set; }
        public Guid IdMedic { get; set; }
    }
}
