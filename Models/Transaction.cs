using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  
    public class Transaction
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string TransactionId { get; set; }
    
        public double Amount { get; set; }
        public double DebitedAmount { get; set; }
        public Transaction(string fromAccount, string toAccount,string bankId, double amount, double debitedAmount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransactionId = "TXN" + bankId + fromAccount + DateTime.Today.ToString("ddMMyy") + DateTime.Now.ToString("hhmmss");
         
            Amount = amount;
            DebitedAmount = debitedAmount;
        }
    }
}
