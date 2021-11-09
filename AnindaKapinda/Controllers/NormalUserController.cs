using AnindaKapinda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AnindaKapinda.Models.ViewModels;
using AnindaKapinda.Extensions;

namespace AnindaKapinda.Controllers
{
    [Authorize(Roles = "normal")]
    public class NormalUserController : Controller
    {
        readonly UserManager<User> _userManager;
        ConnectionDbContext context;
        
        public NormalUserController(UserManager<User> userManager, ConnectionDbContext dbContext)
        {
            context = dbContext;
            _userManager = userManager;
        }

        public IActionResult ProductList(string id)
        {
            var userId = GetUserId();
            BindCart();
            List<Address> myAddress = new List<Address>();
            myAddress = context.Addresses.Where(a => a.UserId == userId).ToList();
            if (myAddress.Count == 0)
            {
                ViewBag.Mesaj = "Lütfen sisteme adresinizi ekleyin.";
                return RedirectToAction("Addresses");
            }
            else
            {
                ProductListViewModel model = new ProductListViewModel();
                model.Categories = context.Categories.Select(a => new Category()
                {
                    CategoryId = a.CategoryId,
                    CategoryName = a.CategoryName
                }).ToList();
                if (id == null)//ilk giriş demek
                {
                    model.Products = context.Products.Where(a=>a.IsActive==true).Select(a => new Product()
                    {
                        ProductId = a.ProductId,
                        ProductName = a.ProductName,
                        Price = a.Price,
                        DiscountedPrice = a.DiscountedPrice,
                        Description = a.Description,
                        IsActive = a.IsActive
                    }).ToList();
                }
                else if (int.TryParse(id, out int categoryID))//kategori secilmisse
                {
                    model.Products = context.Products.Where(a => a.Category.CategoryId == categoryID && a.IsActive == true).Select(a => new Product()
                    {
                        ProductId = a.ProductId,
                        ProductName = a.ProductName,
                        Price = a.Price,
                        DiscountedPrice = a.DiscountedPrice,
                        Description = a.Description,
                        IsActive = a.IsActive
                    }).ToList();
                }
                else
                { RedirectToAction("Error"); }
                return View(model);
            }
           
        }
        public IActionResult Profile()
        {
            int userId = GetUserId();
            if (userId != 0)
            {
                User user = new User();
                user = context.Users.SingleOrDefault(a => a.Id == userId);
                if (user != null)
                {
                    return View(user);
                }
            }
            return View();
        }
        public IActionResult EditProfile()
        {
            int userId = GetUserId();
            if (userId != 0)
            {
                User user = new User();
                user = context.Users.SingleOrDefault(a => a.Id == userId);
                if (user != null)
                {
                    return View(user);
                }
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı bulunamadı.";
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditProfile(User _user)
        {
            int userId = GetUserId();
            if (userId != 0)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Mesaj = "Kayıt tamamlanamadı";
                    return View(_user);
                }
                else
                {
                    User user = new User();
                    user = context.Users.SingleOrDefault(a => a.Id == userId);
                    user.Name = _user.Name;
                    user.Surname = _user.Surname;
                    user.PhoneNumber = _user.PhoneNumber;
                    user.BirthDate = _user.BirthDate;
                    try
                    {
                        context.Users.Update(user);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Mesaj = "Kayıt tamamlanamadı";
                        return View(_user);
                    }

                }


            }
            return RedirectToAction("Profile");

        }

        
        public IActionResult Addresses()
        {
            var userId = GetUserId();
            List<Address> myAddress = new List<Address>();
            myAddress = context.Addresses.Where(a => a.UserId == userId).ToList();

            return View(myAddress);

        }
        public IActionResult AddAddress()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAddress(Address _address)
        {
            var userId = GetUserId();
            if (ModelState.IsValid)
            {
                Address myAddress = new Address();
                myAddress.UserId = userId;
                myAddress.AddressField = _address.AddressField;
                myAddress.District = _address.District;
                myAddress.City = _address.City;
                myAddress.IsActive = _address.IsActive;
                DeactiveAddress();
                try
                {
                    context.Addresses.Add(myAddress);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Mesaj = "Kayıt tamamlanamadı";
                    return View(_address);
                }
                return RedirectToAction("Addresses");
            }
            return View(_address);
        }
        public IActionResult EditAddress(string id)
        {
            Address address = new Address();
            if (int.TryParse(id, out int addressID))
            {
                address = context.Addresses.SingleOrDefault(a => a.AddressId == addressID);
                if (address != null)
                {
                    return View(address);
                }
                else
                {
                    ViewBag.Mesaj = "Adres bulunamadı.";
                    return View();
                }
            }
                
            return View();
        }
        [HttpPost]
        public IActionResult EditAddress(Address _address)
        {
            var userId = GetUserId();
            if (ModelState.IsValid)
            {
                Address address = new Address(); 
                address = context.Addresses.SingleOrDefault(a => a.AddressId == _address.AddressId);
                if (address != null)
                {
                    address.UserId = userId;
                    address.AddressField = _address.AddressField;
                    address.City = _address.City;
                    address.District = _address.District;
                    address.IsActive = _address.IsActive;
                    DeactiveAddress();
                    try
                    {
                        context.Addresses.Update(address);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Mesaj = "Kayıt tamamlanamadı";
                        return View(_address);
                    }
                    return RedirectToAction("Addresses");
                }
               
            }
            return View(_address);
        }
        public IActionResult DeleteAddress(string id)
        {
            Address address = new Address();
            if (int.TryParse(id, out int addressID))
            {
                address = context.Addresses.SingleOrDefault(a => a.AddressId == addressID);
                if (address != null)
                {
                    try
                    {
                        context.Addresses.Remove(address);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Mesaj = "Adres silinemedi";
                         return RedirectToAction("Addresses");
                    }

                    return RedirectToAction("Addresses");
                }
                else
                {
                    ViewBag.Mesaj = "Adres bulunamadı.";
                    return RedirectToAction("Addresses");
                }
            }

            return RedirectToAction("Addresses");

        }
        public IActionResult PaymentMethods()
        {
            var userId = GetUserId();
            List<PaymentMethod> myPayment = new List<PaymentMethod>();
            myPayment = context.PaymentMethods.Where(a => a.UserId == userId).ToList();
            return View(myPayment);
        }
        public IActionResult AddPaymentMethod()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPaymentMethod(PaymentMethod _paymentMethod)
        {
            var userId = GetUserId();
            if (ModelState.IsValid)
            {
                PaymentMethod payment = new PaymentMethod();
                payment.UserId = userId;
                payment.CardName = _paymentMethod.CardName;
                payment.CardNumber = _paymentMethod.CardNumber;
                payment.IsActive = _paymentMethod.IsActive;

                DeactiveCard();
                try
                {
                    context.PaymentMethods.Add(payment);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Mesaj = "Kayıt tamamlanamadı";
                    return View(_paymentMethod);
                }
                return RedirectToAction("PaymentMethods");
            }
            return View(_paymentMethod);
        }
        public IActionResult EditPaymentMethod(string id)
        {
            PaymentMethod payment = new PaymentMethod();
            if (int.TryParse(id, out int paymentID))
            {
                payment = context.PaymentMethods.SingleOrDefault(a => a.PaymantMethodId == paymentID);
                if (payment != null)
                {
                    return View(payment);
                }
                else
                {
                    ViewBag.Mesaj = "Kart bulunamadı.";
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditPaymentMethod(PaymentMethod _paymentMethod)
        {
            var userId = GetUserId();
            if (ModelState.IsValid)
            {
                PaymentMethod payment = new PaymentMethod();
                payment = context.PaymentMethods.SingleOrDefault(a => a.PaymantMethodId == _paymentMethod.PaymantMethodId);
                if (payment != null)
                {
                    payment.UserId = userId;
                    payment.CardName = _paymentMethod.CardName;
                    payment.CardNumber = _paymentMethod.CardNumber;
                    payment.IsActive = _paymentMethod.IsActive;
                    DeactiveCard();
                    try
                    {
                        context.PaymentMethods.Update(payment);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Mesaj = "Kayıt tamamlanamadı";
                        return View(_paymentMethod);
                    }
                    return RedirectToAction("PaymentMethods");
                }

            }
            return View(_paymentMethod);

        }
        public IActionResult DeletePaymentMethod(string id)
        {
            PaymentMethod payment = new PaymentMethod();
            if (int.TryParse(id, out int paymentID))
            {
                payment = context.PaymentMethods.SingleOrDefault(a => a.PaymantMethodId == paymentID);
                if (payment != null)
                {
                    try
                    {
                        context.PaymentMethods.Remove(payment);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Mesaj = "Kart silinemedi";
                        return RedirectToAction("PaymentMethods");
                    }

                    return RedirectToAction("PaymentMethods");
                }
                else
                {
                    ViewBag.Mesaj = "Kart bulunamadı.";
                    return RedirectToAction("PaymentMethods");
                }
            }
            return RedirectToAction("PaymentMethods");
        }

        public IActionResult Orders()
        {
            var userId = GetUserId();
            OrderViewModel model = new OrderViewModel();
            model.Orders = context.Orders.Where(a => a.User.Id == userId).OrderByDescending(a => a.OrderDate).ToList();
            model.OrderDetails = context.OrderDetails.Where(a => a.Order.User.Id == userId).OrderByDescending(a=>a.OrderId).ToList();
           
            var OrderTotals = context.OrderDetails.Where(a => a.Order.User.Id == userId)
            .GroupBy(a => a.OrderId).OrderByDescending(a => a.Key)
            .Select(a => new OrderTotal()
            {
                orderId = a.Key,
                subTotal = a.Sum(q => q.Price * q.Quantity)
            }).ToList();
            model.OrderTotals = OrderTotals;
            return View(model);

        }
        public IActionResult OrderDetails(string id)
        {

            MarketOrderInfoViewModel marketOrder = new MarketOrderInfoViewModel();
            List<OrderInfoViewModel> order = new List<OrderInfoViewModel>();
            if (int.TryParse(id, out int orderID))
            {
                order = context.OrderDetails.Join(context.Orders, od => od.OrderId, o => o.OrderId, (od, o) => new { od, o })
                    .Join(context.Products, odo => odo.od.ProductId, p => p.ProductId, (odo, p) => new { odo, p })
                    .Join(context.Users, odop => odop.odo.od.Order.User.Id, u => u.Id, (odop, u) => new { odop, u })
                    .Join(context.Addresses, odopu => odopu.odop.odo.od.Order.Address.AddressId, a => a.AddressId, (odopu, a) => new { odopu, a })
                    .Where(x => x.odopu.odop.odo.od.OrderId == orderID)
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
                        Status=y.odopu.odop.odo.od.Order.Status,
                        Price=y.odopu.odop.p.Price
                        
                    }).ToList();

                marketOrder.Name = order[0].Name;
                marketOrder.Surname = order[0].Surname;
                marketOrder.OrderId = order[0].OrderId;
                marketOrder.PhoneNumber = order[0].PhoneNumber;
                marketOrder.AddressField = order[0].AddressField;
                marketOrder.City = order[0].City;
                marketOrder.District = order[0].District;
                marketOrder.Status = order[0].Status;
                
                marketOrder.OrderInfoViewModels = order;
            }
                return View(marketOrder);
        }
        public void DeactiveCard()
        {
            var userId = GetUserId();
            PaymentMethod payment = new PaymentMethod();
            payment = context.PaymentMethods.SingleOrDefault(a => a.UserId == userId && a.IsActive == true);
            if (payment != null)
            {
                payment.IsActive = false;
                try
                {
                    context.PaymentMethods.Update(payment);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Mesaj = "Bir hata oluştu";
                }
            }
        }
        public void DeactiveAddress()
        {
            var userId = GetUserId();
            Address address = new Address();
            address = context.Addresses.SingleOrDefault(a => a.UserId == userId&& a.IsActive==true);
            if (address != null)
            {
                address.IsActive = false;
                try
                {
                    context.Addresses.Update(address);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Mesaj = "Bir hata oluştu";
                   
                }
                
            }
           
        }
        public int GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int userid))
            {
                return userid;
            }
            else return 0;
        }
        void BindCart()
        {
            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("cart");




            //List<CartItem> cart = context.Suppliers.Select(a => new SelectListItem()
            //{
            //    Value = a.SupplierId.ToString(),
            //    Text = a.CompanyName
            //}).ToList();
            ViewBag.Cart = cart;

        }
    }
}
