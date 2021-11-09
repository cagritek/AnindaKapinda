using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models
{
    public class PaymentMethod
    {
        [Key]
        
        public int PaymantMethodId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string CardName { get; set; }
        [Required]
        public string CardNumber { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; }
    }
}
