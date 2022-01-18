using FiorelloDataFromDb.Models;
using FiorelloDataFromDb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser user = new AppUser
            {
                Fullname=register.Fullname,
                UserName=register.Username,
                Email=register.Email
            };
            IdentityResult result =await _userManager.CreateAsync(user, register.Password);
            //Model state data annotationlari yoxlayir,amma startupda qeyd elediyimiz
            //passwordu uzunlugu ve s nin neticesi bu resultdan gelir
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                   
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Show()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
    }
}
