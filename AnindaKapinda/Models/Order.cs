using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        
        public int OrderId { get; set; }



        public DateTime? OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }


        public int? ShipperId { get; set; }


        public int? MarketMemberId { get; set; }

        public string? Status { get; set; }


        [ForeignKey("UserId")]
        [Column(Order =1)]
       
        public User User { get; set; }
        
        [ForeignKey("AddressId")]
        [Column(Order = 2)]
        
        public Address Address { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public static implicit operator List<object>(Order v)
        {
            throw new NotImplementedException();
        }
    }
}
