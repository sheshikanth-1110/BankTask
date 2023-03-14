using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Currency
    {
        
        
        public string CurrencyName { get; set; } 
        public double ExchangeRate { get; set; } 

        public Currency(string currencyName, double exchangeRate) 
        { 
            CurrencyName= currencyName;
            ExchangeRate= exchangeRate;
        }
    }
}
