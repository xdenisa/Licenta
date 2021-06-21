using Microsoft.AspNetCore.Http;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models
{
    public class PortfolioViewModel
    {
        public PatientViewModel Patient { get; set; }
        public string Observations { get; set; }
        public IFormFile Document { get; set; }
        public IEnumerable<Result> Portfolio { get; set; }

     
    }
}
