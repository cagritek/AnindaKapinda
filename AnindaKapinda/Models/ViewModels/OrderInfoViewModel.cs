using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models.ViewModels
{
    [NotMapped]
    public class OrderInfoViewModel
    {
        [Key]
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string AddressField { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }


    }
}
