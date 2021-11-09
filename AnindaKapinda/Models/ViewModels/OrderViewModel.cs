using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Orders = new List<Order>();
            OrderDetails = new List<OrderDetail>();
            OrderTotals = new List<OrderTotal>();
        }
        public List<OrderTotal> OrderTotals { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
