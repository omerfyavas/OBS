namespace Login.Models
{


    public class StudentNoteModel
    {

        public int NoteId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string StudentSurname { get; set; }

        public int? VisaNote { get; set; }

        public int? FinalNote { get; set; }

        public int? HomeworkNote { get; set; }

        public decimal? AverageNote { get; set; }

        public bool? Status { get; set; }

        public int? Total { get; set; }
    }
}
