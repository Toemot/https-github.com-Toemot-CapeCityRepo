using com.acme.test01.OlasunkanmiOtokiti;
using System;

namespace Work
{
    class Program
    {
        static void Main(string[] args)
        {
            int accountId = 4321;

            try
            {
            Console.WriteLine("Test1: Open a Savings Account with a min balance of R1000");
            SavingsAccount x = new SavingsAccount();
            //CurrentAccount y = new CurrentAccount();
                x.OpenAccount(accountId, 1000);
                //Assert
                if (x.accountId == accountId)
                {
                    Console.WriteLine("Passed Test1- Account Openeed");
                }
                else
                    Console.WriteLine("Test1: Failed");
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Test1: Failed {0}",e);
             }

            try
            {
                Console.WriteLine("Test2: Savings Account not opened if deposit less that R1000");
                SavingsAccount x = new SavingsAccount();
                x.OpenAccount(accountId, 500);
                //Assert
                if (x.accountId != accountId)
                {
                    Console.WriteLine("Passed Test2- Account not Opened");
                }
                else
                    Console.WriteLine("Test2: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test2: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test3: Open a Current Account with no miminum amount");
                CurrentAccount y = new CurrentAccount();
                y.OpenAccount(accountId, 0);
                //Assert
                if (y.accountId == accountId)
                {
                    Console.WriteLine("Passed Test3- Current Account Opened with no Minimum Deposit");
                }
                else
                    Console.WriteLine("Test3: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test3: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test4: Savings Account balance is increased by the amount deposited");
                SavingsAccount x = new SavingsAccount();
                x.OpenAccount(accountId, 1000);
                x.Deposit(accountId, 1000);
                //Assert
                if (x.CurrentBalance == 2000 )
                {
                    Console.WriteLine("Passed Test4- Savings Account balance increased by amount deposited");
                }
                else
                    Console.WriteLine("Test4: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test4: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test5: Ensure that Current Account balance is increased by the amount deposited");
                CurrentAccount y = new CurrentAccount();
                y.OpenAccount(accountId, 0);
                y.Deposit(accountId, 20000);
                //Assert
                if (y.CurrentBalance == 20000)
                {
                    Console.WriteLine("Passed Test5- Current Account balance increased by the amount deposited");
                }
                else
                    Console.WriteLine("Test5: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test5: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test6: Ensure that Savings Account balance is decreased by the amount withdrawn");
                SavingsAccount x = new SavingsAccount();
                x.OpenAccount(accountId, 1000);
                x.Deposit(accountId, 1000);
                x.Withdraw(accountId, 500);
                //Assert
                if (x.CurrentBalance == 1500)
                {
                    Console.WriteLine("Passed Test6- Savings Account balance decreases by the amount withdrawn");
                }
                else
                    Console.WriteLine("Test6: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test6: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test7: Ensure that Current Account can have both positive balance");
                CurrentAccount y = new CurrentAccount();
                y.OpenAccount(accountId, 0);
                y.Deposit(accountId, 1000);
                //Assert
                if (y.CurrentBalance == 1000)
                {
                    Console.WriteLine("Passed Test7- Current Account has a positive balance");
                }
                else
                    Console.WriteLine("Test7: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test7: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test8: Ensure that Current Account has a negative balance");
                CurrentAccount y = new CurrentAccount();
                y.OpenAccount(accountId, 0);
                y.Withdraw(accountId, 500);
                //Assert
                if (y.CurrentBalance == -500)
                {
                    Console.WriteLine("Passed Test8- Current Account balance can take a negative balance");
                }
                else
                    Console.WriteLine("Test8: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test8: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test9: Ensure that maximum amount OD for Current Account is R100,000.00");
                CurrentAccount y = new CurrentAccount();
                y.OpenAccount(accountId, 0);
                y.Withdraw(accountId, 100000);
                //Assert
                if (y.CurrentBalance == -100000)
                {
                    Console.WriteLine("Passed Test9- Maximum OD amount for Current Account is R100,000");
                }
                else
                    Console.WriteLine("Test9: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test: Failed {0}", e);

            }

            try
            {
                Console.WriteLine("Test10: Ensure that Current Account cannot be overdrawn by the balance + OD limit");
                CurrentAccount y = new CurrentAccount();
                y.OpenAccount(accountId, 0);
                y.Deposit(accountId, 20000);
                y.Withdraw(accountId, 40000);
                //Assert
                if (y.CurrentBalance == -20000)
                {
                    Console.WriteLine("Passed Test10- Current Account cannot be overdrawn by the balance + OD limit");
                }
                else
                    Console.WriteLine("Test10: Failed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Test: Failed {0}", e);

            }
        }
    }
}
