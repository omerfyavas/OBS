using Login.Data;
using Login.Domain;
using Login.Models;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context.Student.ToList();

                return View(students);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentModel model)
        {
            if (model.StudentName == null)
            {
                TempData["Hata"] = "Lütfen geçerli isim giriniz";
                return RedirectToAction("Student");
            }
            if (model.StudentSurname == null)
            {
                TempData["Hata"] = "Lütfen geçerli soyisim giriniz";
                return RedirectToAction("Student");
            }
            using (var context = new ApplicationDbContext())
            {
                context.Student.Add(new Student { Name = model.StudentName, Surname = model.StudentSurname });
                context.SaveChanges();
            }
            return View("Student","List");
        }

    }
}
