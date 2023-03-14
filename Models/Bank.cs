using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bank
    {
        public List<Employee> EmployeeList = new List<Employee>(); 
        public List<User> UserList = new List<User>();
        public Dictionary<string, double> DifferentCurrencies = new Dictionary<string, double>() { { "INR", 1 }, { "USD", 80 }, { "EUR", 87 } };
        public string BankName { get; set; }

        public ServiceCharges CurrentBankServiceCharges { get; set; }
        public ServiceCharges OtherBankServiceCharges { get; set; }
        public Currency currency { get; set; }
        public string BankId { get; set; }
        public Bank(string bankName)
        {
            BankName = bankName;
            BankId = bankName.Substring(0, 3) + DateTime.Today.ToString("ddMMyy");
            currency = new Currency("INR", 1);
            CurrentBankServiceCharges = new ServiceCharges(0, 5);
            OtherBankServiceCharges = new ServiceCharges(2, 6);

        }
        public void AddEmployee(string name, string passWord, string bankName, int index, long phoneNumber)
        {
            Employee newEmployee = new Employee(name,passWord,bankName,index,phoneNumber);
            EmployeeList.Add(newEmployee);
        }
        public void AddUser(string name, string passWord, string bankName, int index, long phoneNumber) 
        {
            User newUser = new User(name, phoneNumber, passWord, index, bankName);
            UserList.Add(newUser);
            newUser.createAccount();
        }
        public void AddNewCurrency(string currencyName, double exchnageRate) 
        {
           
            DifferentCurrencies.Add(currencyName, exchnageRate);
        }
        public void AddServiceChargeForCurrentBank(int RTGS,int IMPS)
        {
            CurrentBankServiceCharges.RTGS = RTGS;
            CurrentBankServiceCharges.IMPS= IMPS;
        }
        public void AddServiceChargeForOtherBanks(int RTGS,int IMPS)
        {
            OtherBankServiceCharges.RTGS= RTGS;
            OtherBankServiceCharges.IMPS= IMPS;
        }
        
        
    }
}
