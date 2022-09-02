using Login.Data;
using Login.Domain;
using Login.Models;
using Microsoft.AspNetCore.Mvc;
namespace Login.Controllers


{
    public class LessonController : Controller
    {
        [HttpGet]
        public IActionResult Lessons()
        {

            using (var context = new ApplicationDbContext())
            {
                var lessons = context.Lesson.ToList();

                return View(lessons);
            }
        }     
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateLesson(LessonModel model)
        {
            if (model.LessonName == null)
            {
                TempData["Hata"] = "Lütfen ders adı giriniz";
                return RedirectToAction("Create");
            }
            if (model.Credit < 1)

            {
                TempData["Hata"] = "Kredi giriniz";
                return RedirectToAction("Create");
            }


            using (var context = new ApplicationDbContext())
            {
                context.Lesson.Add(new Lesson { Credit = model.Credit, Name = model.LessonName });
                context.SaveChanges();

            }
            return View("Lesson","Lesson");

        }
    }
}
