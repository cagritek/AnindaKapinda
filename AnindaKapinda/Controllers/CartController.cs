using AnindaKapinda.Extensions;
using AnindaKapinda.Models;
using AnindaKapinda.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnindaKapinda.Controllers
{
	[Authorize(Roles = "normal")]
	public class CartController : Controller
	{
		readonly UserManager<User> _userManager;
		ConnectionDbContext context; 
        public CartController(UserManager<User> userManager, ConnectionDbContext dbContext)
        {
			_userManager = userManager;
			context = dbContext;
        }
		public IActionResult AddToCart(string input)
		{
			
			string _productID = input.Substring(0, input.IndexOf("-"));
			string _quantity = input.Substring(input.IndexOf("-") + 1);


			if (IsInputCorrect(input))
			{
				int productID = Convert.ToInt32(_productID);
				int quantity = Convert.ToInt32(_quantity);
				Product p = context.Products.SingleOrDefault(a => a.ProductId == productID);

				if (p != null)
				{
					List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("cart");
					if (cart != null)
					{
						CartItem cartItem = cart.SingleOrDefault(a => a.ProductID == p.ProductId);
						if (cartItem != null)
						{
							cartItem.Quantity = quantity;
						}
						else
                        {
                            if (p.DiscountedPrice!=null)
                            {
								cartItem = new CartItem()
								{
									ProductID = p.ProductId,
									Name = p.ProductName,
									Price = p.DiscountedPrice.Value,
									Quantity = quantity
								};
								cart.Add(cartItem);
							}
                            else
                            {
								cartItem = new CartItem()
								{
									ProductID = p.ProductId,
									Name = p.ProductName,
									Price = p.Price,
									Quantity = quantity
								};
								cart.Add(cartItem);
							}
						}
					}
					else
					{
						cart = new List<CartItem>();

						if (p.DiscountedPrice != null)
						{
							CartItem cartItem = new CartItem()
							{
								ProductID = p.ProductId,
								Name = p.ProductName,
								Price = p.DiscountedPrice.Value,
								Quantity = quantity
							};
							cart.Add(cartItem);
						}
                        else
                        {
							CartItem cartItem = new CartItem()
							{
								ProductID = p.ProductId,
								Name = p.ProductName,
								Price = p.Price,
								Quantity = quantity
							};
							cart.Add(cartItem);
						}
					}
					HttpContext.Session.Set("cart", cart);
					decimal cartSum = 0;
					if (cart != null)
					{
						foreach (var item in cart)
						{
							cartSum += item.Price * (Convert.ToDecimal( item.Quantity));
						}
					}
					return Json(cartSum);
				}
				else
				{
					return Json("Ürün bulunamadı");
				}
			}
			else
			{
				return Json("Ürün bulunamadı");
			}
		}

		public IActionResult CartList()
        {
			List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("cart");
			return View(cart);
        }
		public IActionResult DeleteFromCart(string id)
        {
			List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("cart");
			if (int.TryParse(id, out int productID))
            {
				
				CartItem cartItem = cart.SingleOrDefault(a => a.ProductID == productID);
				cart.Remove(cartItem);
				HttpContext.Session.Set("cart", cart);
				return RedirectToAction("CartList");
			}
           
				ViewBag.Message = "Ürün Bulunamadı.";
			    return	RedirectToAction("CartList");
        }

		public IActionResult Payment()
        {
			int userid = GetUserId();

			PaymentMethod payment = context.PaymentMethods.SingleOrDefault(a => a.UserId == userid&& a.IsActive==true);
            if (payment != null)
            {
				return View(payment);
            }
            else
            {
				ViewBag.Message = "Sisteme kayıtlı ödeme kartınız bulunmuyor. Lütfen siparişi tamamlamak için ödeme yöntemi ekleyiniz";
				return View();
            }
		
        }
		public IActionResult MakeOrder()
		{
			int userid = GetUserId();

			User user = new User();
            user = context.Users.Single(a => a.Id == userid);
			
            Address address = new Address();
            address = context.Addresses.Single(a => a.UserId == userid && a.IsActive == true);
            Order order = new Order();
           order.User = user;
           order.Address = address;
            order.OrderDate = DateTime.Now;
            order.Status = "Hazırlanıyor.";
			
			List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("cart");
            List<OrderDetail> orderdetail = new List<OrderDetail>(); 
			if (cart != null)
            {
				foreach (var item in cart)
				{
					OrderDetail od = new OrderDetail();

					od.ProductId = item.ProductID;
					od.Quantity = (short)item.Quantity;
					od.Price = item.Price;
					order.OrderDetails.Add(od);
				}
				try
				{
					context.Orders.Add(order);
					context.SaveChanges();
				}
				catch (Exception)
				{

					ViewBag.Message = "Sipariş tamamlanamadı.";
					return RedirectToAction("CartList");
				}
			}
           
			HttpContext.Session.Clear();
			return RedirectToAction("Orders", "NormalUser");

        }

		public bool IsInputCorrect(string input)
		{
			string _productID = input.Substring(0, input.IndexOf("-"));
			string _quantity = input.Substring(input.IndexOf("-") + 1);

			if (int.TryParse(_productID, out int int1))
			{
				if (int.TryParse(_quantity, out int int2))
				{
					return true;
				}
				else { return false; }
			}
			else { return false; }
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
	}
}
