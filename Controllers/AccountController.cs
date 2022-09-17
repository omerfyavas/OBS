using Login.Data;
using Login.Domain;
using Login.Models;
using Microsoft.AspNetCore.Mvc;

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



    }
}
