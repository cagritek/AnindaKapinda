using AnindaKapinda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminEmployeeController : Controller
    {
        readonly UserManager<User> _userManager;
        ConnectionDbContext context;
        public AdminEmployeeController(UserManager<User> userManager, ConnectionDbContext dbContext)
        {
            context = dbContext;
            _userManager = userManager;
        }
        public IActionResult EmployeeList(string roleId)
        {
            List<User> users = new List<User>();
            if (roleId == null)//ilk giriş demek
            {
               users  = context.Users.ToList();
            }
            else if (int.TryParse(roleId, out int roleID))//tür secilmisse
            {
                users = context.Users.Where(a => a.RoleId == roleID).Select(a => new Models.User()
                {
                    Id=a.Id,
                    Name=a.Name,
                    Surname=a.Surname,
                    BirthDate=a.BirthDate,
                    Email=a.Email,
                    PhoneNumber=a.PhoneNumber,
                    RoleId=a.RoleId
                }).ToList();
            }
            else
            {
                RedirectToAction("Error");
            }
                return View(users);
        }
        public IActionResult EmployeeCreate()
        {
            List<SelectListItem> roles = context.Roles.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeCreate(User _user)
        {
            var user = new User
            {
                UserName = _user.Email,
                Email = _user.Email,
                Name = _user.Name,
                Surname = _user.Surname,
                PhoneNumber = _user.PhoneNumber,
                BirthDate = _user.BirthDate,
                RoleId=_user.RoleId
            };
            var result = await _userManager.CreateAsync(user, _user.PasswordHash);

            if (result.Succeeded)
            {
                var foundbymail = await _userManager.FindByEmailAsync(user.Email);
                if (_user.RoleId == 3)
                {
                    var result1 = await _userManager.AddToRoleAsync(foundbymail, "shipper");
                    if (result1.Succeeded)
                    {
                        return RedirectToAction("EmployeeList");
                    }
                }
                else if(_user.RoleId == 4)
                {
                    var result1 = await _userManager.AddToRoleAsync(foundbymail, "marketmember");
                    if (result1.Succeeded)
                    {
                        return RedirectToAction("EmployeeList");
                    }
                    else
                    {
                        ViewBag.Message = "Çalışan eklenemedi lütfen Çalışan bilgilerini kontrol ediniz.";
                        return View(_user);
                    }
                }
            }
            else
            {
                ViewBag.Message = "Çalışan eklenemedi lütfen Çalışan bilgilerini kontrol ediniz.";
                return View(_user);
            }
            return View();
        }
        public IActionResult EmployeeDelete(int id)
        {
            User user = new User();
            user = context.Users.SingleOrDefault(a => a.Id==id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();

            }
            return RedirectToAction("EmployeeList");
        }
    }
}
