using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models
{
    public class OrderDetail
    {
       // [Key]
        
        [ForeignKey("Order")]
       // [Key, Column(Order = 0), ForeignKey("Order")]
        public int OrderId { get; set; }
        //   [Key]

         [ForeignKey("Product")]
       // [Key, Column(Order = 0), ForeignKey("Product")]
        public int ProductId { get; set; }

        public decimal Price { get; set; }
        public short Quantity { get; set; }
       // public decimal? DiscountedPrice { get; set; }

        public  Order Order { get; set; }
        public  Product Product { get; set; }
    }
}
