namespace Models
{
    public class Account
    {
        public double Amount { get; set; }
        public string AccountNumber { get; set; }
        

        public Account(string name)
        {
            Amount = 0;
            AccountNumber = name + DateTime.Today.ToString("ddMMyy") + DateTime.Now.ToString("hhmmss");
           
        }
    }
}