using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class MedicProfileViewModel
    {
        public IFormFile Image { get; set; }
        public MedicViewModel Medic { get; set; }
        public IEnumerable<SelectListItem> Specializations { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
