using System;

namespace SimpleConsoleWorks
{
    public class Program 
    {
        static void Main(string[] args)
        {
            int accountId = 12345;

            try
            {
                Console.WriteLine("Open Savings acount with minimum of 1000");
                SavingsAccount savings = new SavingsAccount();
                savings.Open(accountId, 1000);
                if (savings.accountId == accountId)
                {
                    Console.WriteLine("Test 1 passed");
                }
                else 
                {
                    Console.WriteLine("Test 1 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 1 failed", e);
            }

            try
            {
                Console.WriteLine("Savings account cannot be opened with less than 1000");
                SavingsAccount savings = new SavingsAccount();
                savings.Open(accountId, 200);
                if (savings.accountId != accountId)
                {
                    Console.WriteLine("Test 2 passed");
                }
                else
                {
                    Console.WriteLine("Test 2 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 2 failed", e);
            }

            try
            {
                Console.WriteLine("Open current account with no minimum amount");
                CurrentAccount current = new CurrentAccount();
                current.Open(accountId, 0);
                if (current.accountId == accountId)
                {
                    Console.WriteLine("Test 3 passed");
                }
                else
                {
                    Console.WriteLine("Test 3 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 3 failed", e);
            }

            try
            {
                Console.WriteLine("Saving account is increased by the amount deposited");
                SavingsAccount savings = new SavingsAccount();
                savings.Open(accountId, 1000);
                savings.Deposit(accountId, 1000);
                if (savings.currentBalance == 2000)
                {
                    Console.WriteLine("Test 4 passed");
                }
                else
                {
                    Console.WriteLine("Test 4 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 4 failed", e);
            }

            try
            {
                Console.WriteLine("Current account is increased by the amount deposited");
                CurrentAccount current = new CurrentAccount();
                current.Open(accountId, 0);
                current.Deposit(accountId, 1000);
                if (current.currentBalance == 1000)
                {
                    Console.WriteLine("Test 5 passed");
                }
                else
                {
                    Console.WriteLine("Test 5 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 5 failed", e);
            }

            try
            {
                Console.WriteLine("Savings account is reduced by the amount withdrawn");
                SavingsAccount savings = new SavingsAccount();
                savings.Open(accountId, 1000);
                savings.Deposit(accountId, 1000);
                savings.Withdraw(accountId, 500);
                if (savings.currentBalance == 1500)
                {
                    Console.WriteLine("Test 6 passed");
                }
                else
                {
                    Console.WriteLine("Test 6 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 6 failed", e);
            }

            try
            {
                Console.WriteLine("Current account has positive balance");
                CurrentAccount current = new CurrentAccount();
                current.Open(accountId, 0);
                current.Deposit(accountId, 1000);
                if (current.currentBalance == 1000)
                {
                    Console.WriteLine("Test 7 passed");
                }
                else
                {
                    Console.WriteLine("Test 7 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 7 failed", e);
            }

            try
            {
                Console.WriteLine("Current account has negative balance");
                CurrentAccount current = new CurrentAccount();
                current.Open(accountId, 0);
                current.Deposit(accountId, 1000);
                current.Withdraw(accountId, 2000);
                if (current.currentBalance == -1000)
                {
                    Console.WriteLine("Test 8 passed");
                }
                else
                {
                    Console.WriteLine("Test 8 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 8 failed", e);
            }

            try
            {
                Console.WriteLine("Maximum amount OD for current account is 100000");
                CurrentAccount current = new CurrentAccount();
                current.Open(accountId, 0);
                current.Deposit(accountId, 0);
                current.Withdraw(accountId, 100000);
                if (current.currentBalance == -100000)
                {
                    Console.WriteLine("Test 9 passed");
                }
                else
                {
                    Console.WriteLine("Test 9 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 9 failed", e);
            }

            try
            {
                Console.WriteLine("Current account cannot be overdrawn by balance + OD");
                CurrentAccount current = new CurrentAccount();
                current.Open(accountId, 0);
                current.Deposit(accountId, 20000);
                current.Withdraw(accountId, 40000);
                if (current.currentBalance == -20000)
                {
                    Console.WriteLine("Test 10 passed");
                }
                else
                {
                    Console.WriteLine("Test 10 failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test 10 failed");
            }
            Console.ReadLine();
        }
    }
}
