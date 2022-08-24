namespace Login.Services
{
    public class AccountService : IAccountService
    {
        private List<Account> accounts;

        public AccountService()
        {
            accounts = new List<Account>();
            {
                new Account
                {
                    UserName = "omer",
                    Password = "12345",
                };
            }
        }

        public bool Login(string userName, string password)
        {

            if (userName != "omer" || password != "123456")
            {
                return false;
            }

            return true;

        }
    }
}
