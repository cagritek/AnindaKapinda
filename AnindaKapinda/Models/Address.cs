using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string AddressField { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
    }
}
