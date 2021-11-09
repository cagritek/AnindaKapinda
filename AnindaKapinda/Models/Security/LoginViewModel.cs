using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models.Security
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email alanı boş geçilemez.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        public string Password { get; set; }
    }
}
