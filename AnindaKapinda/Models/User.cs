using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models
{
    public class User :IdentityUser<int>
    {
        public User()
        {
            PaymentMethods = new HashSet<PaymentMethod>();
            Addresses = new HashSet<Address>();
            Orders = new HashSet<Order>();
        }
        [Required(ErrorMessage = "Ad boş geçilemez.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad boş geçilemez.")]
        public string Surname { get; set; }
        [ForeignKey("UserRole")]
        public int? RoleId { get; set; }

        [Required(ErrorMessage = "Doğum tarihi boş geçilemez.")]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }
        public ICollection<PaymentMethod> PaymentMethods { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public UserRole UserRole { get; set; }

    }
}
