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
                var students = context.Student.OrderByDescending(p => p.Id).ToList();

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
                return RedirectToAction("Create");
            }
            if (model.StudentSurname == null)
            {
                TempData["Hata"] = "Lütfen geçerli soyisim giriniz";
                return RedirectToAction("Create");
            }
            using (var context = new ApplicationDbContext())
            {
                context.Student.Add(new Student { Name = model.StudentName, Surname = model.StudentSurname });
                context.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var existingStudent = context.Student.Where(p => p.Id == id).FirstOrDefault();

                if (existingStudent == null)
                {
                    TempData["Hata"] = "Kayıt bulunamadı";
                    return View("Update");
                }

                var studentModel = new StudentModel();

                studentModel.Id = id;
                studentModel.StudentName = existingStudent.Name;
                studentModel.StudentSurname = existingStudent.Surname;

                return View("Update", studentModel);

            }
        }
        [HttpPost]
        public IActionResult Update(StudentModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var existingStudent = context.Student.Where(p => p.Id == model.Id).FirstOrDefault();

                if (existingStudent == null)
                {
                    TempData["Hata"] = "Kayıt bulunamadı";
                    return View("Update");
                }

                existingStudent.Name = model.StudentName;
                existingStudent.Surname = model.StudentSurname;

                context.Student.Update(existingStudent);
                context.SaveChanges();

            }
            TempData["Bilgi"] = "Güncelleme Başarılı";
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var existingStudent=context.Student.Where(p=>p.Id == id).FirstOrDefault();
                if (existingStudent == null)
                {
                    TempData["Hata"] = "Kayıt bulunamadı";
                    return View("Delete");

                }
                var studentModel=new StudentModel();
                studentModel.Id = id;
                studentModel.StudentName = existingStudent.Name;
                studentModel.StudentSurname = existingStudent.Surname;

                return View("Delete", studentModel);

            }

        }
        [HttpPost]
        public IActionResult Delete(StudentModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var existingStudent = context.Student.Where(p => p.Id == model.Id).FirstOrDefault();

                if (existingStudent == null)
                {
                    TempData["Hata"] = "Kayıt bulunamadı";
                    return View("Delete");
                }

                existingStudent.Name = model.StudentName;
                existingStudent.Surname = model.StudentSurname;

                context.Student.Remove(existingStudent);
                context.SaveChanges();

            }
            TempData["Bilgi"] = "Öğrenci Silindi";
            return RedirectToAction("List");
        }



    }
}
