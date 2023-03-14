using Services;
using Models;
using System;
using System.Reflection;
using HelperProject;

namespace AppUi
{
    public class ApplicationUi
    {
        List<Bank> bankList = new List<Bank>();
        BankService bankService = new BankService();
        EmployeeService employeeService = new EmployeeService();
      
        UserService userService = new UserService();
        public void StartAppUi()
        {
            
            int option = 1;
           
            while (option != 5)
            {
                Console.WriteLine();
                Console.WriteLine("1.Add Bank");
                Console.WriteLine("2.Add Employee");
                Console.WriteLine("3.Employee Login");
                Console.WriteLine("4.User Login");
                Console.WriteLine("5.Exit");
                if(int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Enter Bank Name");
                            string bankName = Console.ReadLine();

                            if (bankName.IsNameValid())
                            {
                                if(bankList.Count!=0 && !bankService.IsValidBank(bankName,bankList))
                                {
                                    Console.WriteLine("Please enter different name");
                                    break;
                                }
                                Bank newBank = new Bank(bankName.ToUpper());
                                bankList.Add(newBank);
                                Console.WriteLine(newBank.BankName + " bank is successfully created :)");
                            }
                            else
                            {
                                Console.WriteLine("INVALID BANK NAME!!!");
                                break;
                            }
                            if (bankList.Count > 0)
                            {

                                Console.WriteLine(bankList[bankList.Count - 1].BankName + "  " + bankList[bankList.Count - 1].BankId);
                            }


                            break;
                        case 2:
                            if (bankList.Count > 0)
                            {
                                bool bankExists = false;
                                Console.WriteLine("Enter Bank Name");
                                string enteredBankName = Console.ReadLine().ToUpper();
                                foreach (Bank bank in bankList)
                                {
                                    if (bank.BankName == enteredBankName)
                                    {
                                        int indexOfEmployee = bank.EmployeeList.Count;
                                        bankExists = true;
                                        Console.WriteLine("Enter Employee Name");
                                        string staffName = Console.ReadLine();
                                        Console.WriteLine("Enter Phone Number:");

                                        long phoneNumber;
                                        if (long.TryParse(Console.ReadLine(), out phoneNumber) && Convert.ToString(phoneNumber).Length == 10)
                                        {
                                            if (bankService.IsExistingEmployee(phoneNumber, bank))
                                            {
                                                Console.WriteLine("THERE IS ALREADY A MEMBER WITH THAT PHONENUMBER");
                                            }
                                            else
                                            {
                                                if (staffName.IsNameValid())
                                                {
                                                    Console.WriteLine("Enter New Password");
                                                    string staffPassword = Console.ReadLine();
                                                    staffPassword.Replace(" ", "");
                                                    if (staffPassword.Count() > 3)
                                                    {

                                                        bank.AddEmployee(staffName, staffPassword, enteredBankName, indexOfEmployee, phoneNumber);
                                                        Console.WriteLine("Account Successfully Created :)");
                                                        Console.WriteLine("       Login Credentials");
                                                        Console.WriteLine("  Staff Login ID = " + bank.EmployeeList[bank.EmployeeList.Count - 1].LoginId);
                                                        Console.WriteLine("  Password = " + bank.EmployeeList[bank.EmployeeList.Count - 1].Password);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Password Should be of length atleast 4 without any spaces!!!");
                                                    }

                                                }
                                                else
                                                {
                                                    Console.WriteLine("ËNTER VALID STAFF NAME!!!");
                                                    break;
                                                }

                                                
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter valid phone number");
                                        }

                                    }
                                }
                                if (!bankExists)
                                {
                                    Console.WriteLine("THERE IS NO BANK EXISTS WITH SUCH NAME!!!");
                                }

                            }
                            else
                            {
                                Console.WriteLine("NO BANK YET HAS BEEN INITIALIZED!!!");
                            }
                            break;
                        case 3:
                            if (bankList.Count > 0)
                            {
                                Console.WriteLine("Enter Employee ID");
                                string employeeID = Console.ReadLine();
                                Console.WriteLine("Enter Password");
                                string passWord = Console.ReadLine();
                                if(!employeeID.IsValidLoginId() || !passWord.IsPasswordValid())
                                {
                                    Console.WriteLine("Enter valid login credentials!!!");
                                    break;
                                }
                                int atRateIndex = employeeID.IndexOf("@");
                                int employeeIndex = Convert.ToInt32(employeeID.Substring(3, atRateIndex - 3));
                                IEnumerable<Bank> bank = Enumerable.Where(bankList, bank => bank.BankId.Substring(0, 3).ToLower() == employeeID.Substring(atRateIndex + 1, 3).ToLower());
                                if (!bank.Any())
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }
                                else if (bank.ElementAt(0).EmployeeList.Count <= employeeIndex )
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }
                                else if (bank.ElementAt(0).EmployeeList[employeeIndex].LoginId.ToLower() != employeeID.ToLower() || bank.ElementAt(0).EmployeeList[employeeIndex].Password != passWord)
                                {
                                   
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }
                                Console.WriteLine("     Hi " + bank.ElementAt(0).EmployeeList[employeeIndex].Name);
                                Console.WriteLine("Choose any of the below option");

                             
                                
                                EmployeeOptions(bank.ElementAt(0));
                               
                            }
                            else
                            {
                                Console.WriteLine("No Bank has initialized yet");
                            }
                            break;
                        case 4:
                            if (bankList.Count > 0)
                            {
                                Console.WriteLine("Enter Login ID:");
                                string loginID = Console.ReadLine();
                                Console.WriteLine("Enter PassWord:");
                                string password = Console.ReadLine();
                              
                                if(!loginID.IsValidLoginId() || !password.IsPasswordValid())
                                {
                                    Console.WriteLine("Enter valid login credentials!!!");
                                    break;
                                }
                                int atTheRateIndex = loginID.IndexOf("@");
                                int index = Convert.ToInt32(loginID.Substring(3,atTheRateIndex-3));
                                IEnumerable<Bank> bank = Enumerable.Where(bankList, bank => bank.BankId.Substring(0,3).ToLower() == loginID.Substring(atTheRateIndex+1,3).ToLower());
                                if(!bank.Any())
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }
                                else if(bank.ElementAt(0).UserList.Count <= index) 
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }
                                else if (bank.ElementAt(0).UserList[index].LoginId.ToLower() != loginID.ToLower() || bank.ElementAt(0).UserList[index].Password != password)
                                {
                                    
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }
                                Console.WriteLine("     Hi " + bank.ElementAt(0).UserList[index].Name + " choose any of the following options");
                               
                              
                                User currentUser = bankService.GetUser(loginID, bank.ElementAt(0));
                              
                                UserOptions(bank.ElementAt(0), currentUser);





                            }
                            else
                            {
                                Console.WriteLine("NO BANK HAS BEEN INITIALIZED YET!!!");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Are you sure you want to exit application???(y/n)");
                            string response = Console.ReadLine().ToLower();
                            if (response.IsValidResponse())
                            {
                                if (response == "n")
                                    option = 1;
                            }
                            else
                            {
                                Console.WriteLine("Enter valid response");
                                option = 1;
                            }
                            break;
                        default:
                            Console.WriteLine("ENTER VALID OPTION!!!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter valid input!!!");
                    option = 1;
                }
                
                
            }
        }
        public void EmployeeOptions(Bank currentBank)
        {
         
            employeeService.CurrentBank = currentBank;
            
            int option = 1;
            while (option != 9)
            {
                Console.WriteLine();
                Console.WriteLine("1.Create Account");
                Console.WriteLine("2.Update Account");
                Console.WriteLine("3.Delete Account");
                Console.WriteLine("4.Add new Currency and Exchange Rate");
                Console.WriteLine("5.Add Service Charge to this bank");
                Console.WriteLine("6.Add Service charge to other bank");
                Console.WriteLine("7.View Transaction");
                Console.WriteLine("8.Revert Transaction");
                Console.WriteLine("9.Log Out");
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Enter User Name:");
                            string userName = Console.ReadLine();
                            if (!userName.IsNameValid())
                            {
                                Console.WriteLine("Enter valid User Name");
                                break;
                            }
                            Console.WriteLine("Enter PhoneNumber:");
                            string tempPhoneNumber = Console.ReadLine();
                            long phoneNumber = tempPhoneNumber.IsPhoneNumberValid();

                            if (phoneNumber == 0)
                            {
                                Console.WriteLine("Enter valid phone number");
                                break;
                            }
                            User newUser = employeeService.CreateNewAccount(userName,phoneNumber);
                            // 
                            if(newUser == null)
                            {
                                Console.WriteLine("Enter valid passWord");
                                break;
                            }
                            else if (newUser.accountList.Count>1)
                            {
                            
                                        Console.WriteLine("Account Successfully Created :)");
                                        Console.WriteLine("Your new Account Number = " + newUser.accountList[newUser.accountList.Count - 1].AccountNumber);
                                    
                            }
                            else
                            {
                                
                               
                                Console.WriteLine("Account Successfully Created :)");
                                Console.WriteLine("User Name = " + newUser.LoginId);
                                Console.WriteLine("Password = " + newUser.Password);
                                Console.WriteLine("Account Number = " + newUser.accountList[0].AccountNumber);


                            }



                            break;
                        case 2:
                            Console.WriteLine("Enter User Id");
                            string userLoginID = Console.ReadLine();
                            Console.WriteLine("Enter Current Password");
                            string password = Console.ReadLine();

                            User requiredUser = bankService.GetUser(userLoginID, currentBank);
                            if (requiredUser == null)
                            {
                                Console.WriteLine("invalid user id");
                                break;
                            }
                            if (requiredUser.Password != password)
                            {
                                Console.WriteLine("Incorrect Password");
                                break;

                            }
                            Console.WriteLine("Enter new Password");
                            string newPassword = Console.ReadLine();
                            if (!newPassword.IsPasswordValid())
                            {
                                Console.WriteLine("Pass word should be atleast 4 letters long");
                            }
                            else if (newPassword.IsPasswordValid() && newPassword == password)
                            {
                                Console.WriteLine("This password is existing password!!! Kindly enter new password");
                                break;
                            }
                            employeeService.ChangePassWord(newPassword, requiredUser);
                            Console.WriteLine("PassWord updated Successfully");

                            break;
                        case 3:
                            Console.WriteLine("Enter User ID:");
                            string userID = Console.ReadLine();
                            User user = bankService.GetUser(userID, currentBank);
                           
                            if (user == null)
                            {
                                Console.WriteLine("There is no user with that user ID");
                                break;
                            }

                            Console.WriteLine("Enter account number you want to delete:");
                            string deleteAccountNo = Console.ReadLine();
                            Account deleteAccount = userService.GetAccount(user, deleteAccountNo);
                           
                            if (deleteAccount == null)
                            {
                                Console.WriteLine("Account doesnot exists");
                                break;
                            }


                            employeeService.DeleteAccount(user, deleteAccount);
                            Console.WriteLine(deleteAccountNo + " is deleted successfully");

                            break;
                        case 4:


                            Console.WriteLine("Enter New Currency Name:");
                            string newCurrencyName = Console.ReadLine();
                            Console.WriteLine("Enter New Excahnge Rate:");
                            string tempNewExchangeRate = Console.ReadLine();
                            double newExchangeRate;
                            if (!double.TryParse(tempNewExchangeRate, out newExchangeRate))
                            {
                                Console.WriteLine("Enter valid exchange rate");
                                break;
                            }
                            if (!newCurrencyName.IsCurrencyNameCodeValid())
                            {
                                Console.WriteLine("Enter valid Currency Name");
                                break;
                            }
                            employeeService.AddCurrencyAndExchangeRate(newCurrencyName, newExchangeRate);

                            Console.WriteLine("Successfully added new currency and exchange rate");
                            break;
                        case 5:
                            Console.WriteLine("Current Bank RTGS Service Charge: " + currentBank.CurrentBankServiceCharges.RTGS);
                            Console.WriteLine("Current Bank IMPS Service Charge: " + currentBank.CurrentBankServiceCharges.IMPS);
                            Console.WriteLine("Do You Really want to change Current Bank Service Charges???(y/n)");
                            string answer = Console.ReadLine();
                            if (!answer.IsValidResponse())
                            {
                                Console.WriteLine("Enter Valid Response");
                                break;
                            }
                            if (answer == "n") break;
                            Console.WriteLine("Enter new RTGS Service Charge for current bank:");

                            string tempRTGS = Console.ReadLine();
                            Console.WriteLine("Enter new IMPS Service Charge for current bank:");
                            string tempIMPS = Console.ReadLine();
                            if (!tempRTGS.IsValidServiceCharge() || !tempIMPS.IsValidServiceCharge())
                            {
                                Console.WriteLine("Enter valid service charges");
                                break;
                            }

                            employeeService.AddServiceChargeForCurrentBank(Convert.ToInt32(tempRTGS), Convert.ToInt32(tempIMPS));
                            Console.WriteLine("Service charges for Current Bank updated successfully");



                            break;
                        case 6:
                            Console.WriteLine("Current Bank RTGS Service Charge: " + currentBank.OtherBankServiceCharges.RTGS);
                            Console.WriteLine("Current Bank IMPS Service Charge: " + currentBank.OtherBankServiceCharges.IMPS);
                            Console.WriteLine("Do You Really want to change Other Bank Service Charges???(y/n)");
                            string choice = Console.ReadLine();
                            if (!choice.IsValidResponse())
                            {
                                Console.WriteLine("Enter Valid Response");
                                break;
                            }
                            if (choice == "n") break;
                            Console.WriteLine("Enter new RTGS Service Charge for current bank:");

                            string tempOtherRTGS = Console.ReadLine();
                            Console.WriteLine("Enter new IMPS Service Charge for current bank:");
                            string tempOtherIMPS = Console.ReadLine();
                            if (!tempOtherRTGS.IsValidServiceCharge() || !tempOtherIMPS.IsValidServiceCharge())
                            {
                                Console.WriteLine("Enter valid service charges");
                                break;
                            }
                            employeeService.AddServiceChargeForOtherBank(Convert.ToInt32(tempOtherRTGS), Convert.ToInt32(tempOtherIMPS));
                            Console.WriteLine("Service charges for Other Banks updated successfully");
                            break;
                        case 7:
                            Console.WriteLine("Enter Login ID: ");
                            string loginId = Console.ReadLine();


                            User trancactionHistoryUser = bankService.GetUser(loginId, currentBank);
                            if (trancactionHistoryUser == null)
                            {
                                Console.WriteLine("Invalid Credentials!!!");
                                break;
                            }
                            if (trancactionHistoryUser.transactionList.Count > 0)
                            {
                                foreach (Transaction transaction in trancactionHistoryUser.transactionList)
                                {
                                    if (transaction.ToAccount == null)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(Convert.ToString(transaction.Amount) + " INR is credited in " + transaction.FromAccount);
                                        Console.WriteLine("Transaction Id: " + transaction.TransactionId);
                                    }
                                    else
                                    {
                                        Console.WriteLine();


                                        Console.WriteLine(Convert.ToString(transaction.Amount) + "INR is transferred from " + transaction.FromAccount + " to " + transaction.ToAccount);



                                        Console.WriteLine("Transaction Id: " + transaction.TransactionId);

                                    }




                                }
                            }
                            else
                            {
                                Console.WriteLine("You have not done any transaction yet");
                            }



                            break;
                        case 8:
                            Console.WriteLine("Enter User Id");
                            string loginID = Console.ReadLine();


                            User revertUser = bankService.GetUser(loginID, currentBank);
                            if (revertUser == null)
                            {
                                Console.WriteLine("invalid user id");
                                break;
                            }
                            Console.WriteLine("Enter transaction id");
                            string transactionID = Console.ReadLine();
                            Transaction transactions = bankService.GetTransaction(revertUser,transactionID);
                            Console.WriteLine("Enter account number that you have used for the transaction:");
                            string revertUserAccountNumber = Console.ReadLine();
                            Account revertAccount = userService.GetAccount(revertUser, revertUserAccountNumber);
                            if (revertAccount == null  )
                            {
                                Console.WriteLine("Enter valid account number ");
                                break;
                            }
                            if(transactions.ToAccount == null || transactions.ToAccount == revertUser.Name ||transactions.ToAccount == revertAccount.AccountNumber || (revertAccount.AccountNumber != transactions.FromAccount && revertAccount.AccountNumber != transactions.ToAccount))
                            {
                                Console.WriteLine("Transactio cannot be reverted");
                                break;
                            }
                     
                            User receiver = bankService.GetReceiver(transactions.ToAccount, bankList);
                            Account receiverAccount = userService.GetAccount(receiver, transactions.ToAccount);
                            Account senderAccount = userService.GetAccount(revertUser, transactions.FromAccount);
                            if(transactions.ToAccount == senderAccount.AccountNumber)
                            {
                                Console.WriteLine("Cannot revert this transaction as this transaction is not done by you");
                                break;
                            }
                            if (bankService.RevertTransaction(transactions.Amount, receiverAccount, senderAccount))
                            {
                                Transaction transaction = new Transaction(transactions.ToAccount, transactions.FromAccount, currentBank.BankId, transactions.Amount, transactions.Amount);
                                revertUser.transactionList.Add(transaction);
                                receiver.transactionList.Add(transaction);
                                Console.WriteLine("Transaction Successfully reverted");
                            }
                            else
                            {
                                Console.WriteLine("Insuffient Funds in Other Account");
                            }
                            break;
                        case 9:
                            Console.WriteLine("Are you sure you want to logout???(y/n)");
                            string response = Console.ReadLine().ToLower();
                            if (response.IsValidResponse())
                            {
                                if (response == "n")
                                    option = 1;
                            }
                            else
                            {
                                Console.WriteLine("Enter valid response!!!");
                                option = 1;
                            }

                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter Valid Option");
                    option = 1;
                }






            }
        }
        public void UserOptions(Bank bank, User user)
        {
          
            userService.CurrentBank = bank;
            userService.CurrentUser= user;
            employeeService.CurrentBank = bank;

   
        Bank currentBank;
        User currentUser;
       
     
            currentBank = bank;
            currentUser = user;
            int option = 1;
            while (option != 5)
            {
                Console.WriteLine("1.Deposit Amount");
                Console.WriteLine("2.WithDraw Amount");
                Console.WriteLine("3.Transfer Funds");
                Console.WriteLine("4.View Transaction History");
                Console.WriteLine("5.Exit");
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Enter Account Number: ");
                            string accountNumber = Console.ReadLine();
                          
                            Account account = userService.GetAccount(currentUser, accountNumber);
                            if (account == null)
                            {
                                Console.WriteLine("Incorrect Account Number");
                                break;
                            }
                            Console.WriteLine("Enter Amount To Be Deposited:");
                            double depositAmount;
                            if (double.TryParse(Console.ReadLine(), out depositAmount))
                            {
                                Console.WriteLine("Enter the type of currency: ");
                                string currencyType = Console.ReadLine();
                                if(userService.DepositMoney(currencyType.ToUpper(), account,depositAmount))
                                {
                                    Console.WriteLine("Amount Successfully Deposited");


                                    Console.WriteLine("Current Balance = " + account.Amount + " INR");
                                }
                                else
                                {
                                    Console.WriteLine("Entered Currency is not available");
                                    Console.WriteLine("Available Currencies are:");
                                    foreach (KeyValuePair<string, double> pair in currentBank.DifferentCurrencies)
                                    {
                                        Console.WriteLine(pair.Key + " " + pair.Value);
                                    }
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Enter Valid Amount");
                            }


                            break;
                        case 2:
                            Console.WriteLine("Enter Amount you want to withdraw:");
                            double withDrawAmount;
                            if (!double.TryParse(Console.ReadLine(), out withDrawAmount))
                            {
                                Console.WriteLine("Enter valid amount!!!");
                                break;
                            }
                            Console.WriteLine("Enter the account number from which you want to withdraw amount:");


                            string withDrawAccountNumber = Console.ReadLine();
                            
                            int withDrawSuccessful = userService.WithDrawMoney(withDrawAccountNumber, withDrawAmount);
                            if(withDrawSuccessful== 0)
                            {
                                Console.WriteLine("Insuffient Funds");

                            }
                            else if(withDrawSuccessful== -1)
                            {
                                Console.WriteLine("Account doesn't Exist");
                            }
                            else if(withDrawSuccessful == 1)
                            {
                                Console.WriteLine("Don't forget to collect the cash at the counter");
                            }
                            break;
                        case 3:

                            Console.WriteLine("Enter account number");
                            string accountNo = Console.ReadLine();
                            Account transferAccount = userService.GetAccount(currentUser, accountNo);
                            if (transferAccount == null)
                            {
                                Console.WriteLine("Account Number does not exists!!!");
                                break;
                            }
                            Console.WriteLine("Enter account number of receiver:");
                            string receiverAccountNumber = Console.ReadLine();

                            User receiver = bankService.GetReceiver(receiverAccountNumber, bankList);
                            if (receiver == null)
                            {
                                Console.WriteLine("Account number does'nt Exist!!!");
                                break;
                            }
                            Account receiverAccount = userService.GetAccount(receiver, receiverAccountNumber);
                            

                           
                            Console.WriteLine("Enter tax type (RTGS/IMPS)");
                            string taxType = Console.ReadLine().ToUpper();
                            if (taxType.IsValidTaxType() == null)
                            {
                                Console.WriteLine("Enter valid Tax Type");
                                break;
                            }
                            Console.WriteLine("Enter amount to be transferred");
                            string amount = Console.ReadLine();
                            int transferSuccessful = userService.TransferMoney(receiver,transferAccount,receiverAccount,taxType,amount);
                            if(transferSuccessful == 0)
                            {
                                Console.WriteLine("Insuffient Funds!!!");
                            }
                            else if(transferSuccessful == -1)
                            {
                                Console.WriteLine("Enter Valid Amount");
                            }
                            else if(transferSuccessful == 1)
                            {
                                Console.WriteLine("Amount Successfully Transferred");
                                Console.WriteLine("Current Balance = " + Convert.ToString(transferAccount.Amount) + " INR");
                            }
                            break;
                        case 4:
                            if (currentUser.transactionList.Count > 0)
                            {
                                foreach (Transaction transaction in currentUser.transactionList)
                                {
                                    if (transaction.ToAccount == null)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(Convert.ToString(transaction.Amount) + " INR is credited in " + transaction.FromAccount);
                                        Console.WriteLine("Transaction Id: " + transaction.TransactionId);
                                    }
                                    else
                                    {
                                        Account currentUserAccount = userService.GetAccount(currentUser, transaction.FromAccount);
                                        if (currentUserAccount == null)
                                        {
                                            Console.WriteLine();

                                            Console.WriteLine(Convert.ToString(transaction.Amount) + "INR is transferred from " + transaction.FromAccount + " to " + transaction.ToAccount);

                                            Console.WriteLine("Transaction Id: " + transaction.TransactionId);
                                        }
                                        else
                                        {
                                            Console.WriteLine();
                                        
                                            Console.WriteLine(Convert.ToString(transaction.DebitedAmount) + "INR is transferred from " + transaction.FromAccount + " to " + transaction.ToAccount);

                                            Console.WriteLine("Transaction Id: " + transaction.TransactionId);
                                        }
                                      

                                    }





                                }
                            }
                            else
                            {
                                Console.WriteLine("You have not done any transaction yet");
                            }

                            break;
                        case 5:
                            Console.WriteLine("Are you sure you want to logout???(y/n)");
                            string response = Console.ReadLine().ToLower();
                            if (response.IsValidResponse())
                            {
                                if (response == "n")
                                    option = 1;
                            }
                            else
                            {
                                Console.WriteLine("Enter valid response!!!");
                                option = 1;
                            }
                            break;
                        default:
                            Console.WriteLine("Option Out Of Range!!!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter Valid Option!!!");
                    option = 1;
                }
            }
        }
    }
}