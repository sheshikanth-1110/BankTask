using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee:Person
    {
       
        public Employee(string name, string passWord, string bankName, int index, long phoneNumber)
        {
            Name = name;
            LoginId = name.Substring(0, 3) + Convert.ToString(index) + "@" + bankName.Substring(0, 3) + ".in";
            Password = passWord;
            PhoneNumber = phoneNumber;
        }
    }
}
