using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PresLes
{
    class Program
    {
        static void Main(string[] args)
        {
            int accountId = 1234;

            try
            {
                Console.WriteLine("Open Savings Account with a min balance of 1,000");
                SavingsAccount savings = new SavingsAccount();
                savings.OpenAcccount(accountId, 1000);
                if (savings.accountId == accountId)
                {
                    Console.WriteLine("Test1 passed");
                }
                else 
                {
                    Console.WriteLine("Test1 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test1 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Cannot open Savings Account with less than 1,000");
                SavingsAccount savings = new SavingsAccount();
                savings.OpenAcccount(accountId, 100);
                if (savings.accountId != accountId)
                {
                    Console.WriteLine("Test2: Test Passed");
                }
                else
                {
                    Console.WriteLine("Test2: Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test2 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Open Current Account with no minimum balance");
                CurrentAccount current = new CurrentAccount();
                current.OpenAcccount(accountId, 0);
                if (current.accountId == accountId)
                {
                    Console.WriteLine("Test3: Test Passed");
                }
                else
                {
                    Console.WriteLine("Test3: Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test3 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Savings Account is increased by the amount deposited");
                SavingsAccount savings = new SavingsAccount();
                savings.OpenAcccount(accountId, 1000);
                savings.Deposit(accountId, 1000);
                if (savings.CurrentBalance == 2000)
                {
                    Console.WriteLine("Test4: Test Passed");
                }
                else
                {
                    Console.WriteLine("Test 4: Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 4 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Current account increased by the amount deposited");
                CurrentAccount current = new CurrentAccount();
                current.OpenAcccount(accountId, 0);
                current.Deposit(accountId, 1000);
                if (current.CurrentBalance == 1000)
                {
                    Console.WriteLine("Test5: Test Passed");
                }
                else
                {
                    Console.WriteLine("Test5 Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test5 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Saving Account is reduced by the amount withdrawn");
                SavingsAccount savings = new SavingsAccount();
                savings.OpenAcccount(accountId, 1000);
                savings.Deposit(accountId, 1000);
                savings.Withdraw(accountId, 500);
                if (savings.CurrentBalance == 1500)
                {
                    Console.WriteLine("Test6: Test Passed");
                }
                else
                {
                    Console.WriteLine("Test6 Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test6 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Current Account has Positive Account");
                CurrentAccount current = new CurrentAccount();
                current.OpenAcccount(accountId, 0);
                current.Deposit(accountId, 500);
                if (current.CurrentBalance == 500)
                {
                    Console.WriteLine("Test7: Test passes");
                }
                else
                {
                    Console.WriteLine("Test7 Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test7 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Test 8: Current Account with negative balance");
                CurrentAccount current = new CurrentAccount();
                current.OpenAcccount(accountId, 0);
                current.Withdraw(accountId, 1500);
                if (current.CurrentBalance == -1500)
                {
                    Console.WriteLine("Test8 Passes");
                }
                else
                {
                    Console.WriteLine("Test8 Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test8 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Test 9: Maximum amount OD for Current Account is 100,000");
                CurrentAccount current = new CurrentAccount();
                current.OpenAcccount(accountId, 0);
                current.Withdraw(accountId, 100000);
                if (current.CurrentBalance == -100000)
                {
                    Console.WriteLine("Test 9 Passed");
                }
                else
                {
                    Console.WriteLine("Test 9 Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 9 failed {0}", e);
            }

            try
            {
                Console.WriteLine("Current Account balance cannot be overdrawn by balance and OD limit");
                CurrentAccount current = new CurrentAccount();
                current.OpenAcccount(accountId, 0);
                current.Deposit(accountId, 10000);
                current.Withdraw(accountId, 20000);
                if (current.CurrentBalance == -10000)
                {
                    Console.WriteLine("Test 10 Passed");
                }
                else
                {
                    Console.WriteLine("Test 10 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 10 failed {0}", e);
            }

            Console.ReadLine();
        }
        
    }
}

