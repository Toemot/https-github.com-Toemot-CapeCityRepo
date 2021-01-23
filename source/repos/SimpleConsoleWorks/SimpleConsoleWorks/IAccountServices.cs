using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleWorks
{
    interface IAccountServices
    {
        void Open(long accountId, int amount);
        void Deposit(long accountId, int amount);
        void Withdraw(long accountId, int amount);
    }

    public class SavingsAccount : IAccountServices
    {
        //public int amountToDeposit;
        public long accountId = 0;
        public int currentBalance = 0;
        private const int _minimumBalance = 1000;
        public bool Opened = false;

        public void Deposit(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new ArgumentException("Please reconfirm your account");

            if (amount <= 0)
                throw new Exception("Your deposit amount must be above 0");

            currentBalance += amount;
        }

        public void Open(long accountId, int amount)
        {
            if (this.accountId != 0)
                throw new ArgumentException("Account Already Opened");

            //if (amount < _minimumBalance)
            //    throw new Exception("Deposit amount must be more that 1000");

            if (amount >= _minimumBalance)
            {
                this.accountId = accountId;
                Opened = true;
            }

            currentBalance += amount;
        }

        public void Withdraw(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new ArgumentException("Please confirm your account");

            if (amount <= 0)
                throw new Exception("Invalid Withdrawal amount");

            var newBalance = currentBalance - amount;
            if (newBalance < _minimumBalance)
                throw new Exception("You cannot withdraw less than the minimum balance");

            currentBalance = newBalance;
        }
    }

    public class CurrentAccount : IAccountServices
    {
        public long accountId = 0;
        public int currentBalance = 0;
        private const int OverDraft = -100000;
        public bool Opened = false;

        public void Deposit(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new ArgumentException("Please confirm account number");

            if (amount < 0)
                throw new Exception("Invalid deposit amount");

            currentBalance += amount;
        }

        public void Open(long accountId, int amount)
        {
            if (this.accountId != 0)
                throw new ArgumentException("Account already opened");

            if (amount >= 0)
            {
                currentBalance += amount;
                this.accountId = accountId;
                Opened = true;
            }
        }

        public void Withdraw(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new ArgumentException("Please reconfirm account");

            if (amount <= OverDraft)
                throw new Exception("Withdrawal limit below OD limit");

            currentBalance -= amount;
        }
    }
}
