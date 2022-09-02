using Login.Data;
using Login.Domain;
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
            return View("Index", "test");
        }


        public IActionResult ConfidentialData()
        {
            return View();
        }

        public IActionResult Notes()
        {

            using (var context = new ApplicationDbContext())
            {
                var notes = context.Note.ToList();

                return View(notes);
            }
        }





        [HttpGet]
        public IActionResult Addnote()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNote(NoteModel model)
        {
            if (model.VisaNote < 0)
            {
                TempData["Hata"] = "Lütfen Geçerli Bir Sayı Giriniz";
                return RedirectToAction("AddNote");
            }
            if (model.FinalNote < 0)
            {
                TempData["Hata"] = "Lütfen Geçerli Bir Sayı Giriniz";
                return RedirectToAction("AddNote");
            }
            if (model.HomeworkNote < 0)
            {
                TempData["Hata"] = "Lütfen Geçerli Bir Sayı Giriniz";
                return RedirectToAction("AddNote");
            }

            using (var context = new ApplicationDbContext())
            {
                context.Note.Add(new Note { VisaNote = (short)model.VisaNote, FinalNote = (short)model.FinalNote, HomeworkNote = (short)model.HomeworkNote });
                context.SaveChanges();
                return View("AddNote");
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