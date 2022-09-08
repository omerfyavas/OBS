namespace Login.Domain
{
    public class Note
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public short VisaNote { get; set; }

        public short FinalNote { get; set; }

        public short HomeworkNote { get; set; }

        public decimal AverageNote { get; set; }

        public bool Status { get; set; }

        public short Total { get; set; }
    }
}
