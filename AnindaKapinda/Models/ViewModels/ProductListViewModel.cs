using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            Products = new List<Product>();
            Categories = new List<Category>();
        }
       
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
