using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace Services
{
    public class UserService
    {
      
        BankService bankService = new BankService();
       
      
       public Bank CurrentBank;
    public    User CurrentUser;
       
        public Account GetAccount(User user, string accountNumber)
        {
            IEnumerable<Account> account = Enumerable.Where(user.accountList, account => account.AccountNumber == accountNumber);
            if (account.Any())
            {
                return account.ElementAt(0);
            }
            return null;
        }
       
        public bool DepositMoney(string currencyType, Account account, double depositAmount)
        {
            double currencyValue ;
            try
            {
                Console.WriteLine(currencyType);
                currencyValue = CurrentBank.DifferentCurrencies[currencyType];
                
           }
            catch
          {
               
                return false;
           }
            double depositedAmount = bankService.DepositAmount(depositAmount, account, currencyValue);
            Transaction transaction = new Transaction(account.AccountNumber, null, CurrentBank.BankId, depositedAmount, 0);
            CurrentUser.transactionList.Add(transaction);
            return true;
        }
        public int WithDrawMoney(string withDrawAccountNumber, double amount)
        {
            Account withDrawAccount = GetAccount(CurrentUser, withDrawAccountNumber);
            if (withDrawAccount == null)
            {
                return -1;
            }
            if (bankService.WithDrawAmount(amount, withDrawAccount))
            {
               
                Transaction transaction = new Transaction(withDrawAccountNumber, CurrentUser.Name, CurrentBank.BankId, amount, amount);
                CurrentUser.transactionList.Add(transaction);
                Console.WriteLine("Current Balance = " + Convert.ToString(withDrawAccount.Amount) + " INR");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int TransferMoney(User receiver,Account senderAccount, Account receiverAccount, string taxType, string amount)
        {
            EmployeeService employeeService = new EmployeeService();
            employeeService.CurrentBank = CurrentBank;
            bool currentBankUser =employeeService.IsCurrentBankUser(receiver);
            double taxPercent;
            double transferAmount;
            if (currentBankUser)
            {
                if (taxType == "RTGS")
                    taxPercent = CurrentBank.CurrentBankServiceCharges.RTGS;
                else
                    taxPercent = CurrentBank.CurrentBankServiceCharges.IMPS;
            }
            else
            {
                if (taxType == "RTGS")
                    taxPercent = CurrentBank.OtherBankServiceCharges.RTGS;
                else
                    taxPercent = CurrentBank.OtherBankServiceCharges.IMPS;
            }

            if (double.TryParse(amount, out transferAmount))
            {

                double transferredAmount = bankService.TransferAmount(transferAmount, senderAccount, receiverAccount, taxPercent);
                if (transferredAmount!=0)
                {

                    Transaction senderTransaction = new Transaction(senderAccount.AccountNumber, receiverAccount.AccountNumber, CurrentBank.BankId, transferAmount, transferredAmount);
                    CurrentUser.transactionList.Add(senderTransaction);
                    senderTransaction.DebitedAmount = transferredAmount;
                    receiver.transactionList.Add(senderTransaction);
                    return 1;
                   
                }
                else
                {
                    return 0;
                   
                    
                }
            }
            else
            {
                return -1;
            }
        }
      
    }
}
