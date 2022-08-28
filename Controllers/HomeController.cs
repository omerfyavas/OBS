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
                var notes = context.Note.Where(p => p.Status == true).ToList();

                return View(notes);
            }
        }

        public IActionResult Students()
        {

            using (var context = new ApplicationDbContext())
            {
                var students = context.Student.OrderByDescending(p => p.Id).Where(p => p.Name == "Ahmet").ToList();

                return View(students);
            }
        }

        public IActionResult Lessons()
        {

            using (var context = new ApplicationDbContext())
            {
                var lessons = context.Lesson.ToList();

                return View(lessons);
            }
        }
        //    //
        //    [HttpPost]
        //    public IActionResult Post()
        //    {
        //        var context = new ApplicationDbContext();

        //    }
        ////
        [HttpGet]
        public IActionResult AddLesson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLesson(LessonModel model)
        {
             if (model.LessonName == null)
            {
                TempData["Hata"] = "Lütfen ders adı giriniz";
                return RedirectToAction("AddLesson");
            }
            if (model.Credit < 1)
                
            {
                TempData["Hata"] = "Kredi giriniz";
                return RedirectToAction("AddLesson");
            }

            using (var context = new ApplicationDbContext())
            {
                context.Lesson.Add(new Lesson { Credit = model.Credit, Name = model.LessonName });

                context.SaveChanges();


                return View("AddLesson");
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