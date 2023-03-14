using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HelperProject;
namespace Services
{
    public class EmployeeService
    {
        public Bank CurrentBank;
   
        BankService bankService = new BankService();
        
       
       
        public User CreateNewAccount(string userName,long phoneNumber)
        {
            User existingUser = bankService.getExistingUser(phoneNumber, CurrentBank);
            
            if (existingUser != null)
            {
                
             
                        existingUser.createAccount();
                        return existingUser;
                    
            }
            else
            {
                Console.WriteLine("Enter Password");
                string passWord = Console.ReadLine();
                if (!passWord.IsPasswordValid())
                {
                   
                    return null;
                }
                CurrentBank.AddUser(userName, passWord, CurrentBank.BankName, CurrentBank.UserList.Count, phoneNumber);
                return CurrentBank.UserList[CurrentBank.UserList.Count - 1];


            }
        }
        public void ChangePassWord(string passWord,User user )
        {
            user.Password = passWord;
        }
        public void DeleteAccount(User user, Account deleteAccount)
        {
            if (user.accountList.Count == 1)
            {
                CurrentBank.UserList.Remove(user);
            }
            else
            {
                user.accountList.Remove(deleteAccount);
            }
        }
        public void AddCurrencyAndExchangeRate(string currencyName, double exchangeRate)
        {
            CurrentBank.AddNewCurrency(currencyName.ToUpper(), exchangeRate);
        }
        public void AddServiceChargeForCurrentBank(int RTGS, int IMPS)
        {
            CurrentBank.AddServiceChargeForCurrentBank(RTGS, IMPS);
        }
        public void AddServiceChargeForOtherBank(int RTGS, int IMPS)
        {
            CurrentBank.AddServiceChargeForOtherBanks(RTGS, IMPS);
        }
        public bool IsCurrentBankUser(User receiver)
        {
            
            IEnumerable<User> users = from user in CurrentBank.UserList where receiver.LoginId == user.LoginId select user;
            if (users.Any())
            {
                return true;
            }
            return false;
        }





    }
}
