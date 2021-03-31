using Proiect.BusinessLogic;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email obligatoriu!")]
        [StringLength(50, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parolă obligatorie!")]
        [DisplayName("Parolă")]
        [StringLength(50, MinimumLength = 5)]
        [PasswordPropertyText]
        public string Password { get; set; }

        public string IsPacient { get; set; }
        public bool IsLogedIn { get; set; }
        public string Id { get; set; }
        public bool IsAdmin { get; set; }

        public LoginViewModel()
       {
            
       }
    }
}
