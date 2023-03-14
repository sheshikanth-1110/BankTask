using Models;
using System.Text;
using System.Linq;
using System.Transactions;

namespace Services
{
    public class BankService
    {
       
        public bool IsExistingEmployee(long phoneNumber, Bank bank)
        {
            foreach (Employee employee in bank.EmployeeList)
            {
                if (employee.PhoneNumber == phoneNumber)
                {
                    return true;
                }
            }
            return false;
        }
        public User getExistingUser(long phoneNumber, Bank bank)
        {
            
            IEnumerable<User> user = Enumerable.Where(bank.UserList, user => user.PhoneNumber == phoneNumber);
            if (user.Any())
            {
                return user.ElementAt(0);
            }
            return null;

        }
        public User GetUser(string userId, Bank bank)
        {
            IEnumerable<User> users = Enumerable.Where(bank.UserList, user => user.LoginId.ToLower() == userId.ToLower());
            if (users.Any())
            {
                return users.ElementAt(0);
            }
            return null;

        }
        public User GetReceiver(string accountNo, List<Bank> bankList)
        {
            IEnumerable<User> users = null;
            foreach (Bank bank in bankList)
            {

                foreach (User user in bank.UserList)
                {

                    users = from account in user.accountList
                            where account.AccountNumber == accountNo
                            select user;
                    if (users.Any())
                    {
                        return users.ElementAt(0);
                    }

                }
            }

            return null;


        }
        public double DepositAmount(double amount, Account account, double exchangeRate)
        {
            account.Amount += amount * exchangeRate;
            return amount*exchangeRate;

        }
        public bool WithDrawAmount(double amount, Account account)
        {
            if (account.Amount < amount)
            {
                return false;
            }
            account.Amount -= amount;
            return true;
        }
        public double TransferAmount(double amount, Account senderAccount, Account receiverAccount,double tax)
        {
            double finalAmount= amount + amount*(tax/100);
         
            if(senderAccount.Amount < finalAmount) { 
             return 0;
            }
            senderAccount.Amount -= finalAmount;
            receiverAccount.Amount += amount;
            return finalAmount;
            
        }
        public bool RevertTransaction(double amount, Account senderAccount, Account receiverAccount)
        {
            if(senderAccount.Amount < amount)
            {
                return false;
            }
            senderAccount.Amount -= amount;
            receiverAccount.Amount += amount;
            return true;
        }
        public Models.Transaction GetTransaction(User user, string transactionID)
        {
          
           IEnumerable<Models.Transaction> transactions = from transaction in user.transactionList where transaction.TransactionId == transactionID select transaction;
            if(transactions.Any())
            {
                return transactions.ElementAt(0);
            }
            return null;
        }
        public bool IsValidBank(string bankName, List<Bank> bankList)
        {
            IEnumerable<Bank> banks = from bank in bankList where bank.BankName.Substring(0,3).ToUpper() == bankName.Substring(0,3).ToUpper() select bank;
            if (banks.Any())
            {
               
                return false;
            }
            return true;
        } 
    } 
}