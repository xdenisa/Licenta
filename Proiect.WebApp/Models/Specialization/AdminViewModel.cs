using Microsoft.AspNetCore.Http;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class AdminViewModel
    {
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public IEnumerable<Medic> Medics { get; set; }
        public IEnumerable<Patient> Patients { get; set;
        }

    }
}
