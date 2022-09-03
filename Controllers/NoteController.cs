using Login.Data;
using Login.Domain;
using Login.Models;
using Microsoft.AspNetCore.Mvc;
namespace Login.Controllers
{
    public class NoteController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            using (var context = new ApplicationDbContext())
            {
                var notes = context.Note.ToList();
                return View(notes);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NoteModel model)
        {
            if (model.VisaNote < 0 && model.VisaNote > 100)
            {
                TempData["Hata"] = "Lütfen geçerli sayi giriniz";
                return RedirectToAction("Create");
            }
            if (model.FinalNote < 0 && model.FinalNote > 100)
            {
                TempData["Hata"] = "Lütfen geçerli sayi giriniz";
                return RedirectToAction("Create");
            }
            if (model.HomeworkNote < 0 && model.HomeworkNote > 100)
            {
                TempData["Hata"] = "Lütfen geçerli sayi giriniz";
                return RedirectToAction("Create");
            }
            using (var context = new ApplicationDbContext())
            {
                context.Note.Add(new Note { VisaNote = (short)model.VisaNote, FinalNote = (short)model.FinalNote, HomeworkNote = (short)model.HomeworkNote });
                context.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }

}
