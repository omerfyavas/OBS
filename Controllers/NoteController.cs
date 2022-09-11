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

                var query = (from note in context.Note
                             join student in context.Student on note.StudentId equals student.Id

                             select new StudentNoteModel
                             {
                                 StudentId = student.Id,
                                 StudentName = student.Name,
                                 StudentSurname = student.Surname,
                                 AverageNote = note.AverageNote,
                                 VisaNote = note.VisaNote,
                                 FinalNote = note.FinalNote,
                                 Status = note.Status,
                                 HomeworkNote = note.HomeworkNote,
                                 Total = note.Total,
                                 NoteId = note.Id,
                             }).OrderByDescending(p => p.AverageNote).ToList();


                return View(query);

                //var notes = context.Note.OrderByDescending(p => p.Total).ToList();
                //return View(notes);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentNoteModel model)
        {
            if (model.VisaNote < 0 || model.VisaNote > 100)
            {
                TempData["Hata"] = "Lütfen geçerli sayi giriniz";
                return RedirectToAction("Create");
            }
            if (model.FinalNote < 0 || model.FinalNote > 100)
            {
                TempData["Hata"] = "Lütfen geçerli sayi giriniz";
                return RedirectToAction("Create");
            }
            if (model.HomeworkNote < 0 || model.HomeworkNote > 100)
            {
                TempData["Hata"] = "Lütfen geçerli sayi giriniz";
                return RedirectToAction("Create");
            }
            if (model.StudentId == null)
            {
                TempData["Hata"] = "Lütfen geçerli sayi giriniz";
                return RedirectToAction("Create");
            }

            using (var context = new ApplicationDbContext())
            {
                context.Note.Add(new Note { VisaNote = (short)model.VisaNote, FinalNote = (short)model.FinalNote, HomeworkNote = (short)model.HomeworkNote, StudentId = model.StudentId });

                context.SaveChanges();
                model.Total = model.VisaNote + model.FinalNote + model.HomeworkNote;
                model.AverageNote = model.Total * 0.3M;

                if (model.AverageNote > 50)
                {

                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }

            }

            return RedirectToAction("List");
        }


    }

}
