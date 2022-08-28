using Login.Models;
using Login.Services;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{

    public class KullaniciController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Mesaj = DateTime.Now.Date.ToString();

            return View();
        }

        [HttpPost]
        public IActionResult LoginPost(AccountModel model)
        {

            IAccountService accountService = new AccountService();

            var isUserExists = accountService.Login(model.KullaniciAdi, model.Sifre);

            if (isUserExists)
            {
                HttpContext.Session.SetString("User", model.KullaniciAdi);

                return RedirectToAction("KulanicilaraOzelSayfa");
            }

            TempData["Hata"] = "Bilgileriniz doğrulanamadı.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult KulanicilaraOzelSayfa()
        {
            var user = HttpContext.Session.GetString("User");

            if (user == null)
            {
                TempData["Hata"] = "Lütfen Giriş yapınız.";

                return RedirectToAction("Index");
            }

            return View("KulanicilaraOzelSayfa", user);
        }

        [HttpGet]
        public IActionResult Cikis()
        {
            HttpContext.Session.Remove("User");

            return RedirectToAction("Index");
        }      
      
    }
}
