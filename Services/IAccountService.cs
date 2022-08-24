namespace Login.Services
{
    public interface IAccountService
    {
        public bool Login(string userName, string password);
    }
}
