using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models.ViewModels
{ 
    [NotMapped]
    public class CartItem
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
    }
}
