using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class LoginViewModel : Controller
    {
        public IActionResult Index()
        {
         

            return View();
        }
    }
}
