using Microsoft.AspNetCore.Http;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class PatientProfileViewModel
    {
        public IFormFile Image { get; set; }
        public PatientViewModel Patient { get; set; }     
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
