using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Kategori adı boş geçilemez.")]
        public string CategoryName { get; set; }
       

        public  ICollection<Product> Products { get; set; }
    }
}
