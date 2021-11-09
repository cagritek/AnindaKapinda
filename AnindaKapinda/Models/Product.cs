using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models
{
    public class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Ürün adı boş geçilemez.")]
        public string ProductName { get; set; }
        [ForeignKey("Category")]
        [Column(Order = 1)]
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public int? Quantity { get; set; }

        public  Category Category { get; set; }
        public  ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
