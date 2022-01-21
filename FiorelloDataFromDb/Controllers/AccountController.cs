using FiorelloDataFromDb.Models;
using FiorelloDataFromDb.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                Fullname = register.Fullname,
                UserName = register.Username,
                Email = register.Email
            };
            if (!register.Terms)
            {
                ModelState.AddModelError("Terms", "Do you agree with terms&condition ?");
                return View();
            }
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
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
            //bunu ona gore yazdiq ki adi sehifeden qeydiyyatdan kecen her kes member olsun
            await _userManager.AddToRoleAsync(user, "Member");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");

                return View();
            }

            //ilk true remember me ve ya keep signed ucundur
            //diger false ise coxsayli giris cehdini qeyd elemek buna uygun limitleme ucundu
            //burdaki result mena olaraq user create oluna bilir ya yox onu gosterir
            //Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, true, false);
            //altdaki qisa yazilisdi,birbasa neticeseni gonderirik
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, login.KeepSigned, true);
            if (!result.Succeeded)
            {
                //bu istifadeci coxsayli giris cehdi edibse ona uygun limitlemedir
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account has been blocked for 5 minutes due to overtrying");
                    return View();
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            //AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        //bunu ona gore qeyd etdik ki,edit ede bilmek ucun mentiqle login oolunmus olmalidi
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM editedUser = new UserEditVM
            {
                Username = user.UserName,
                Email = user.Email,
                Fullname = user.Fullname
            };
            return View(editedUser);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM editedUser)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM eUser = new UserEditVM
            {
                Username = user.UserName,
                Email = user.Email,
                Fullname = user.Fullname
            };

            //burda yoxlayiriq ki ilk once bele bir ad varsa ve deyisdirilmeye calisilirsa xeta versin
            if (user.UserName != editedUser.Username && await _userManager.FindByNameAsync(editedUser.Username) != null)
            {
                ModelState.AddModelError("", $"{editedUser.Username} existed");
                return View(eUser);
            }

            //yeni eger edit hissesinde user current password hissesini doldurubsa 
            //demek passwordu da deyismek fikrindedir,hazirda userin passworla bagli
            //hecneyi deyismek istememesi halina uygun serti yazilib
            if (string.IsNullOrWhiteSpace(editedUser.CurrentPassword))
            {
                user.UserName = editedUser.Username;
                user.Fullname = editedUser.Fullname;
                user.Email = editedUser.Email;
                //burda update olsa da,update olmasini yeniden login olanda goruruk
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.UserName = editedUser.Username;
                user.Fullname = editedUser.Fullname;
                user.Email = editedUser.Email;
               IdentityResult result= await _userManager.ChangePasswordAsync(user,editedUser.CurrentPassword,editedUser.Password);
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(eUser);
                }
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Show()
        {
            return Content(User.Identity.Name.ToString());
        }
    }
}
