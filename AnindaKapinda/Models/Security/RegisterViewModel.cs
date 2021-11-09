using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models.Security
{

    public class RegisterViewModel
    {
        
        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$", ErrorMessage = "Şifreniz minimum 8 karakter olmalıdır ve küçük harf, büyük harf, sayı içermelidir")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$", ErrorMessage ="Şifreniz minimum 8 karakter olmalıdır ve küçük harf, büyük harf, sayı içermelidir")]
     
        [Compare(nameof(Password),ErrorMessage ="Şifreler uyuşmuyor.")]
        public string ConfirmedPassword { get; set; }
        [Required(ErrorMessage = "Ad boş geçilemez.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad boş geçilemez.")]
        public string Surname { get; set; }
        [Required(ErrorMessage ="Lütfen geçerli bir mail adresi giriniz.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir mail adresi giriniz.")]
        [Key]
        public string Email { get; set; }
        [Required(ErrorMessage ="Telefon numarası boş geçilemez.")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Lütfen geçerli bir telefon numarası giriniz")]

        public string Mobile { get; set; }

        [Required(ErrorMessage ="Doğum tarihi boş geçilemez.")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
    }
}
