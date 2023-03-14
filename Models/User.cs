using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User:Person
    {
        public List<Account> accountList = new List<Account>();
       
        public List<Transaction> transactionList = new List<Transaction>();
        public User(string name,long phoneNumber,string passWord,int index,string bankName) 
        { 
            Name = name;
            PhoneNumber = phoneNumber;
            Password = passWord;
            LoginId = name.Substring(0,3) + Convert.ToString(index) + "@" + bankName.Substring(0,3) + ".in" ;
        }
        public void createAccount()
        {
            Account newAccount = new Account(Name.ToUpper().Substring(0, 3));
           
            accountList.Add(newAccount);
        }
        

    }
}
