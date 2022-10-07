using Login.Data;
using Login.Domain;
using Login.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Login.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (model.UserNameSurname == null)
            {
                TempData["Hata"] = "Lütfen Geçerli Bir İsim Giriniz";
                return View("Register");
            }
            if (model.Password == null)
            {
                TempData["Hata"] = "Lütfen Geçerli Bir Şifre Giriniz";
                return View("Register");
            }
            if (model.UserEmail == null)
            {
                TempData["Hata"] = "Lütfen Geçerli Bir E-posta Giriniz";
                return View("Register");
            }
            if (model.Password != model.RePassword)
            {
                TempData["Hata"] = "Hatalı Parola";
                return View("Register");
            }


            using (var context = new ApplicationDbContext())
            {
                var existingAccount = context.Account.Where(p => p.Email == model.UserEmail).FirstOrDefault();
                if (existingAccount != null)
                {
                    TempData["Hata"] = "Kullanıcı Mevcut";
                    return RedirectToAction("Register");
                }

                var newAccount = new Account();

                newAccount.Email = model.UserEmail;
                newAccount.NameSurname = model.UserNameSurname;
                newAccount.Password = model.Password;
                newAccount.Type = 1;

                context.Account.Add(newAccount);
                context.SaveChanges();


            }

            TempData["Bilgi"] = "Kayıt Başarılı";

            return RedirectToAction("Register");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginModel account)
        {
            using (var context = new ApplicationDbContext())
            {
                var existingAccount = context.Account.Where(p => p.Email == account.Email && p.Password == account.Password).FirstOrDefault();

                if (existingAccount == null)
                {
                    TempData["Hata"] = "Kayıt Bulunamadı";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Bilgi"] = "Giriş Başarılı";

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, existingAccount.Email),
                        new Claim(ClaimTypes.Name, existingAccount.NameSurname),

                    };


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {

                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                  new ClaimsPrincipal(claimsIdentity),
                                                  authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
