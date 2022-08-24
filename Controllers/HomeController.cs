using Login.Data;
using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Login.Controllers
{
    public class HomeController : Controller
    {


        
        //GET:Default

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            var tarih = DateTime.Now.ToString();

            var sessionKontrol = HttpContext.Session.GetString("TEST"); 

            if (string.IsNullOrEmpty(sessionKontrol))
            {
                HttpContext.Session.SetString("TEST", DateTime.Now.ToString());
            }

            var session = HttpContext.Session;
            

            


            return View("Index", sessionKontrol);
        }

        
        public IActionResult ConfidentialData()
        {
            return View();
        }

        public IActionResult Notes()
        {

            using(var context = new ApplicationDbContext())
            {
                var notes = context.Note.ToList();

                return View(notes);

            }


        }







        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       

    }
}