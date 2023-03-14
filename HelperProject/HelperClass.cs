using System.Runtime.CompilerServices;
using System.Text;

namespace HelperProject
{
    public static class HelperClass
    {
        
        public static bool IsNameValid(this string name)
        {
            StringBuilder bankNameBuilder = new StringBuilder(name);
            int charCount = 0;
            foreach (char c in name.ToLower())
            {
                if (Convert.ToInt32(c) > 96 && Convert.ToInt32(c) < 123)
                {
                    charCount++;
                }
            }
            if (charCount == bankNameBuilder.Length && charCount > 2)
            {
                return true;
            }
            return false;
        }
        public static bool IsCurrencyNameCodeValid(this string currency)
        {
            int spacePresent = currency.IndexOf(' ');
            if (spacePresent != -1 && currency.Length < 3) return false;
            foreach (char c in currency.ToLower())
            {
                if ((int)c < 97 || (int)c > 122) return false;
            }

            return true;

        }
        public static bool IsValidServiceCharge(this string serviceCharge)
        {
            if (serviceCharge.Length > 2) return false;
            int tempServiceCharge;
            if (int.TryParse(serviceCharge, out tempServiceCharge)) return true;
            return false;
        }
        public static bool IsValidLoginId(this string loginId)
        {

            int atTheRateIndex = loginId.IndexOf("@");
            int dotIndex = loginId.IndexOf(".");
            if (loginId.Length < 11 || atTheRateIndex == -1 || dotIndex == -1 || dotIndex - atTheRateIndex != 4 || loginId.Length - dotIndex != 3) return false;
            
            byte[] asciiValues = ASCIIEncoding.ASCII.GetBytes(loginId.ToLower());
            for (int i = 0; i < loginId.Length; i++)
            {
                
                if (i < 3 || (i > atTheRateIndex && i < dotIndex) || (i > dotIndex && i < loginId.Length))
                {
                    if (asciiValues[i] < 97 || asciiValues[i] > 122)
                    {
                       
                        return false;
                    }
                }
                else if (i >= 3 && i < atTheRateIndex)
                {
                    if (asciiValues[i] < 48 || asciiValues[i] > 57)
                    {
                        
                        return false;
                    }
                }


            }
            return true;
        }
        public static long IsPhoneNumberValid(this string phoneNumber)
        {
            long phoneNo;
            if (long.TryParse(phoneNumber, out phoneNo))
            {
                if (phoneNumber.Length == 10)
                {
                    return phoneNo;
                }

            }
            return 0;
        }
        public static bool IsPasswordValid(this string password)
        {
            if (password == null || password.IndexOf(" ") != -1 || password.Length < 3) return false;
            return true;
        }
        public static bool IsValidResponse(this string response)
        {
            if (response == "y" || response == "n")
            {
                return true;
            }
            return false;
        }
        public static string IsValidTaxType(this string taxType)
        {
            if (taxType.ToLower() == "rtgs" || taxType.ToLower() == "imps")
            {
                return taxType;
            }
            return null;
        }
       
        
      
    }
}