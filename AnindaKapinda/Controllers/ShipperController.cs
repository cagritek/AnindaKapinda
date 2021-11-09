using AnindaKapinda.Models;
using AnindaKapinda.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnindaKapinda.Controllers
{
    [Authorize(Roles = "shipper")]
    public class ShipperController : Controller
    {
        ConnectionDbContext context;
        public ShipperController(ConnectionDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult OrderList()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int _userid = Convert.ToInt32(userid);
            List<Order> order = new List<Order>();
            order = context.Orders.Where(a => a.Status == "Yola Çıktı." && a.ShipperId==_userid).ToList();
            return View(order);
        }
        public IActionResult OrderDetail(string id)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int _userid = Convert.ToInt32(userid);
            MarketOrderInfoViewModel marketOrder = new MarketOrderInfoViewModel();
            List<OrderInfoViewModel> order = new List<OrderInfoViewModel>();
            if (int.TryParse(id, out int orderID))
            {
                order = context.OrderDetails.Join(context.Orders, od => od.OrderId, o => o.OrderId, (od, o) => new { od, o })
                    .Join(context.Products, odo => odo.od.ProductId, p => p.ProductId, (odo, p) => new { odo, p })
                    .Join(context.Users, odop => odop.odo.od.Order.User.Id, u => u.Id, (odop, u) => new { odop, u })
                    .Join(context.Addresses, odopu => odopu.odop.odo.od.Order.Address.AddressId, a => a.AddressId, (odopu, a) => new { odopu, a })
                    .Where(x => x.odopu.odop.odo.od.OrderId == orderID&&x.odopu.odop.odo.o.ShipperId==_userid)
                    .Select(y => new OrderInfoViewModel()
                    {
                        OrderId = y.odopu.odop.odo.od.OrderId,
                        ProductName = y.odopu.odop.odo.od.Product.ProductName,
                        Quantity = y.odopu.odop.odo.od.Quantity,

                        AddressField = y.odopu.odop.odo.od.Order.Address.AddressField,
                        District = y.odopu.odop.odo.od.Order.Address.District,
                        City = y.odopu.odop.odo.od.Order.Address.City,
                        Name = y.odopu.odop.odo.od.Order.User.Name,
                        Surname = y.odopu.odop.odo.od.Order.User.Surname,
                        PhoneNumber = y.odopu.odop.odo.od.Order.User.PhoneNumber,
                    }).ToList();

                marketOrder.Name = order[0].Name;
                marketOrder.Surname = order[0].Surname;
                marketOrder.OrderId = order[0].OrderId;
                marketOrder.PhoneNumber = order[0].PhoneNumber;
                marketOrder.AddressField = order[0].AddressField;
                marketOrder.City = order[0].City;
                marketOrder.District = order[0].District;
                marketOrder.OrderInfoViewModels = order;
                BindStatus();
            }
            return View(marketOrder);
        }
        public IActionResult OrderFinalize(MarketOrderInfoViewModel _order)
        {
            Order order = context.Orders.SingleOrDefault(a => a.OrderId == _order.OrderId);
            if (order != null)
            {
                order.Status = _order.Status;
                order.DeliveryDate = DateTime.Now;
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    context.Update(order);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Message = "Kayıt esnasında bir hata oluştu";
                    return RedirectToAction("OrderList");
                }
            }
            return RedirectToAction("OrderList");
        }
        void BindStatus()
        {
            List<SelectListItem> Status = new List<SelectListItem>();
            Status.Insert(0, new SelectListItem() { Value = "Teslim Edildi.", Text = "Teslim Edildi." });
            Status.Insert(1, new SelectListItem() { Value = "Adres Eksik/Hatalı.", Text = "Adres Eksik/Hatalı." });
            Status.Insert(2, new SelectListItem() { Value = "Üye Adreste Yoktu.", Text = "Üye Adreste Yoktu." });
            ViewBag.Status = Status;

        }
    }
}
