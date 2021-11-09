using AnindaKapinda.Models;
using AnindaKapinda.Models.Security;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnindaKapinda.Controllers
{
    public class SecurityController : Controller
    {

        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;
        readonly RoleManager<UserRole> _roleManager;

        public SecurityController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Mesaj = "Giriş Bilgilerinizi Kontrol Ediniz";
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, "admin"))
                    {
                        
                        return RedirectToAction("CategoryList", "AdminCategory");
                       
                    }

                    else if (await _userManager.IsInRoleAsync(user, "normal"))
                    {

                        return RedirectToAction("ProductList", "NormalUser");

                    }
                    else if (await _userManager.IsInRoleAsync(user, "shipper"))
                    {

                        return RedirectToAction("OrderList", "Shipper");

                    }
                    else if (await _userManager.IsInRoleAsync(user, "marketmember"))
                    {

                        return RedirectToAction("OrderList", "MarketMember");

                    }

                }
            }
            ModelState.AddModelError(string.Empty, "Giriş başarısız.");
            return View(loginViewModel);
        }

    
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Security");
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Mesaj = "Kayıt tamamlanamadı";
            return View(registerViewModel);
        }
           

        var user = new User
        {
            UserName= registerViewModel.Email,
            Email = registerViewModel.Email,
            Name = registerViewModel.Name,
            Surname = registerViewModel.Surname,
            PhoneNumber = registerViewModel.Mobile,
            BirthDate=registerViewModel.BirthDate,
            RoleId=2
            

        };
        var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            
        if (result.Succeeded)
        {
                var foundbymail = await _userManager.FindByEmailAsync(user.Email);
                var result2 =await _userManager.AddToRoleAsync(foundbymail, "normal");
                if (result2.Succeeded)
                {
                    return RedirectToAction("Login", "Security");
                }
                
        }
        return View(registerViewModel);
    }
        public async Task<IActionResult> StartUpProject()
        {// Proje ilk defa ayağa kaklarken roller(admin,normal,shipper,marketmember) ve
         // 1 normal üye,1 admin hesabı,1 market çalışan hesabı ve 2 adet kurye hesabı oluşur.

           //ROLLER
            UserRole identityRole = new UserRole
            {
                Name = "admin"
            };

            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                UserRole identityRole1 = new UserRole
                {
                    Name = "normal"
                };

                IdentityResult result1 = await _roleManager.CreateAsync(identityRole1);
                if (result1.Succeeded)
                {
                    UserRole identityRole2 = new UserRole
                    {
                        Name = "shipper"
                    };

                    IdentityResult result2 = await _roleManager.CreateAsync(identityRole2);
                    if (result2.Succeeded)
                    {
                        UserRole identityRole3 = new UserRole
                        {
                            Name = "marketmember"
                        };

                        IdentityResult result3 = await _roleManager.CreateAsync(identityRole3);
                        if (result3.Succeeded)
                        {

                           
                        }
                    }
                }
            }


            //HESAPLAR

            //ADMIN
            var user1 = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                Name = "admin",
                Surname = "admin",
                PhoneNumber = "123456",
                BirthDate=DateTime.Now,


            };
            var result4 = await _userManager.CreateAsync(user1, "Admin123.");

            if (result4.Succeeded)
            {
                var foundbymailadmin = await _userManager.FindByEmailAsync(user1.Email);
                var result5 = await _userManager.AddToRoleAsync(foundbymailadmin, "admin");
                if (result5.Succeeded)
                {//NORMAL USER
                    var user2 = new User
                    {
                        UserName = "normal@normal.com",
                        Email = "normal@normal.com",
                        Name = "normal",
                        Surname = "user",
                        PhoneNumber = "123456",
                        BirthDate = DateTime.Now,


                    };
                    var result6 = await _userManager.CreateAsync(user2, "Normal123.");

                    if (result6.Succeeded)
                    {
                        var foundbymailuser = await _userManager.FindByEmailAsync(user2.Email);
                        var result7 = await _userManager.AddToRoleAsync(foundbymailuser, "normal");
                        if (result7.Succeeded)
                        {//MARKET MEMBER
                            var user3 = new User
                            {
                                UserName = "marketmember@marketmember.com",
                                Email = "marketmember@marketmember.com",
                                Name = "market",
                                Surname = "member",
                                PhoneNumber = "123456",
                                BirthDate = DateTime.Now,


                            };
                            var result8 = await _userManager.CreateAsync(user3, "Market123.");

                            if (result8.Succeeded)
                            {
                                var foundbymailmarket = await _userManager.FindByEmailAsync(user3.Email);
                                var result9 = await _userManager.AddToRoleAsync(foundbymailmarket, "marketmember");
                                if (result9.Succeeded)
                                {//SHIPPER
                                    var user4 = new User
                                    {
                                        UserName = "shipper@shipper.com",
                                        Email = "shipper@shipper.com",
                                        Name = "shipper",
                                        Surname = "shipper",
                                        PhoneNumber = "123456",
                                        BirthDate = DateTime.Now,


                                    };
                                    var result10 = await _userManager.CreateAsync(user4, "Shipper123.");

                                    if (result10.Succeeded)
                                    {
                                        var foundbymailshipper = await _userManager.FindByEmailAsync(user4.Email);
                                        var result11 = await _userManager.AddToRoleAsync(foundbymailshipper, "shipper");
                                        if (result11.Succeeded)
                                        {//SHIPPER 1
                                            var user5 = new User
                                            {
                                                UserName = "shipper1@shipper1.com",
                                                Email = "shipper1@shipper1.com",
                                                Name = "shipper1",
                                                Surname = "shipper1",
                                                PhoneNumber = "123456",
                                                BirthDate = DateTime.Now,


                                            };
                                            var result12 = await _userManager.CreateAsync(user5, "Shipper123.");

                                            if (result12.Succeeded)
                                            {
                                                var foundbymailshipper1 = await _userManager.FindByEmailAsync(user5.Email);
                                                var result13 = await _userManager.AddToRoleAsync(foundbymailshipper1, "shipper");
                                                if (result13.Succeeded)
                                                {



                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return View();

        }
    }
}
