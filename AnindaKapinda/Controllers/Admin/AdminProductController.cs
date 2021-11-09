using AnindaKapinda.Models;
using AnindaKapinda.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminProductController : Controller
    {
        ConnectionDbContext context;
        public AdminProductController(ConnectionDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult ProductList(string id)
        {
            ProductListViewModel model = new ProductListViewModel();
            model.Categories = context.Categories.Select(a => new Category()
            {
                CategoryId = a.CategoryId,
                CategoryName = a.CategoryName
            } ).ToList();
            if (id == null)//ilk giriş demek
            {
                model.Products = context.Products.Select(a => new Product()
                {
                    ProductId = a.ProductId,
                    ProductName = a.ProductName,
                    Price=a.Price,
                    DiscountedPrice=a.DiscountedPrice,
                    Description=a.Description,
                    IsActive=a.IsActive

                }).ToList();
            }
            else if (int.TryParse(id, out int categoryID))//kategori secilmisse
            {
                model.Products = context.Products.Where(a => a.Category.CategoryId == categoryID).Select(a => new Product()
                {
                    ProductId = a.ProductId,
                    ProductName = a.ProductName,
                    Price = a.Price,
                    DiscountedPrice = a.DiscountedPrice,
                    Description=a.Description,
                    IsActive = a.IsActive
                }).ToList();
            }
            else
            {
                RedirectToAction("Error");
            }
                return View(model);
        }
        public IActionResult ProductCreate()
        {
            BindCategories();
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreate(Product p)
        {
            if (ModelState.IsValid)
            {
                if (IsNameUnique(p))
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                    return RedirectToAction("ProductList");
                }
                else
                {
                    ViewBag.Message = "Bu isimde bir ürün mevcut.Lütfen başka bir ürün adı giriniz.";
                    return View(p);
                }
            }
            return RedirectToAction("ProductCreate");
            
        }

        public IActionResult ProductEdit(string id)
        {

            if (int.TryParse(id, out int ID))
            {
                Product product = new Product();
                product = context.Products.SingleOrDefault(a => a.ProductId == ID);
                BindCategories();
                return View(product);
            }
            else
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult ProductEdit(Product _product)
        {
            if (ModelState.IsValid)
            {
                
                    Product product = new Product();
                    product = context.Products.SingleOrDefault(a => a.ProductId == _product.ProductId);
                if (product.ProductName != _product.ProductName && IsNameUnique(_product))
                {
                    product.ProductName = _product.ProductName;
                    product.CategoryId = _product.CategoryId;
                    product.Price = _product.Price;
                    product.DiscountedPrice = _product.DiscountedPrice;
                    product.Description = _product.Description;
                    context.Products.Update(product);
                    context.SaveChanges();
                    return RedirectToAction("ProductList");
                }
                else if (product.ProductName == _product.ProductName)
                {
                    product.ProductName = _product.ProductName;
                    product.CategoryId = _product.CategoryId;
                    product.Price = _product.Price;
                    product.DiscountedPrice = _product.DiscountedPrice;
                    product.Description = _product.Description;
                    context.Products.Update(product);
                    context.SaveChanges();
                    return RedirectToAction("ProductList");

                }
                else
                {
                    ViewBag.Message = "Bu isimde bir ürün mevcut.Lütfen başka bir ürün adı giriniz.";
                    BindCategories();
                    return View(_product);
                }
                   
              
             
               
            }
          
            
            return RedirectToAction("ProductList");
        } 
        public IActionResult ProductDelete(string id)
        {
            Product product = new Product();
            if (int.TryParse(id, out int ID))
            {
                product = context.Products.SingleOrDefault(a => a.ProductId == ID);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return RedirectToAction("ProductList");
                }
                else
                {
                    ViewBag.Message = "Ürün Bulunamadı";

                    return RedirectToAction("ProductList");
                }
            }
            else { return View("Error");
            }
        }
        public bool IsNameUnique(Product _product)
        {
            Product product = new Product();
            product = context.Products.SingleOrDefault(a => a.ProductName == _product.ProductName);
            if (product != null)
            {
                return false;
            }
            else return true;
        }
        void BindCategories()
        {
            List<SelectListItem> categories = context.Categories.Select(a => new SelectListItem()
            {
                Value = a.CategoryId.ToString(),
                Text = a.CategoryName
            }).ToList();
            ViewBag.Categories = categories;

        }

    }
}
