using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models.ViewModels
{
    public class MarketOrderInfoViewModel
    {
        public MarketOrderInfoViewModel()
        {
           
            OrderInfoViewModels = new List<OrderInfoViewModel>();
        }
        public int OrderId { get; set; }
        public string AddressField { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }

        public User Shipper { get; set; }
        public List<OrderInfoViewModel> OrderInfoViewModels { get; set; }
    }
}
