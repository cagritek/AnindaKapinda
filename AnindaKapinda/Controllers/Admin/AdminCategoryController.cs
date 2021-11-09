using AnindaKapinda.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Controllers
{
    
    [Authorize(Roles ="admin")]
    public class AdminCategoryController : Controller
    {
       
        ConnectionDbContext context;
        public AdminCategoryController(ConnectionDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult CategoryList()
        {
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryCreate(Category _category)
        {
            Category category = new Category();
            if (ModelState.IsValid)
            { 
                if (IsNameUnique(_category))
                {
                    category.CategoryName = _category.CategoryName;
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return RedirectToAction("CategoryCreate");
                }
                else
                {
                    ViewBag.Message = "Bu isimde bir kategori mevcut.Lütfen başka bir kadegori adı giriniz.";
                    return View(_category);
                }
            }
            return RedirectToAction("CategoryCreate");
        }
        public IActionResult CategoryEdit(string id)
        {
            if (int.TryParse(id, out int ID))
            {
                Category category = new Category();
                category = context.Categories.SingleOrDefault(a => a.CategoryId == ID);
                return View(category);
            }
            else
            {
                return View("Error");
            }
           
        }
        [HttpPost]
        public IActionResult CategoryEdit(Category _category)
        {
            if (ModelState.IsValid)
            {
                if (IsNameUnique(_category))
                {
                    Category category = new Category();
                    category = context.Categories.SingleOrDefault(a => a.CategoryId == _category.CategoryId);
                    category.CategoryName = _category.CategoryName;
                    context.Categories.Update(category);
                    context.SaveChanges();
                    return RedirectToAction("CategoryList");
                }
                else
                {
                    ViewBag.Message = "Bu isimde bir kategori mevcut.Lütfen başka bir kategori adı giriniz.";
                    return View(_category);
                }
            }
            return RedirectToAction("CategoryList");
        }
        public IActionResult CategoryDelete(string id)
        {
            if (int.TryParse(id, out int ID))
            {
                Category category = new Category();
                category = context.Categories.SingleOrDefault(a => a.CategoryId == ID);
                context.Categories.Remove(category);
                context.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            else
            {
                return View("Error");
            }
           
        }
        public bool IsNameUnique(Category _category)
        {
            Category category = new Category();
            category = context.Categories.SingleOrDefault(a => a.CategoryName == _category.CategoryName);
            if (category != null)
            {
                return false;
            }
            else return true;
        }
    }
}
