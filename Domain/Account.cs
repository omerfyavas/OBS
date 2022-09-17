namespace Login.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }

    }
}
