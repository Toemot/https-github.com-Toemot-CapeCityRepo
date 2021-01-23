using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresLes
{
    public interface IAccountService
    {
        void OpenAcccount(long accountId, int amountToDeposit);
        void Deposit(long accountId, int amount);
        void Withdraw(long accountId, int amount);
    }

    public class CurrentAccount : IAccountService
    {
        public int CurrentBalance = 0;
        private const int OverDraftLimit = -1000000;
        public bool Opened = false;
        public long accountId;

        public void Deposit(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);

            if (amount <= 0)
                throw new Exception("Invalid Deposit Amount");

            CurrentBalance = amount + CurrentBalance;
        }

        public void OpenAcccount(long accountId, int amountToDeposit)
        {
            if (this.accountId != 0)
                throw new Exception("Account Already Opened");

            if (amountToDeposit < 0)
                throw new Exception("Low Deposit, Account cannot be opened");

            this.accountId = accountId;
            CurrentBalance = amountToDeposit;
            Opened = true;
        }

        public void Withdraw(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);

            if (amount <= 0)
                throw new Exception("Amount too low to withdraw");

            var newBalance = CurrentBalance - amount;

            if (newBalance >= OverDraftLimit)
            {
                CurrentBalance = newBalance;
            }
            else
            {
                throw new WithdrawalAmountTooLargeException(amount);
            }
        }
    }

    public class SavingsAccount : IAccountService
    {
        public long accountId = 0;
        public int CurrentBalance = 0;
        private const int MinimumBalance = 1000;

        public void Deposit(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);

            if (amount <= 0)
                throw new Exception("Invalid Deposit Amount");

            CurrentBalance = amount + CurrentBalance;
        }

        public void OpenAcccount(long accountId, int amountToDeposit)
        {
            if (this.accountId != 0)
                throw new Exception("Account Already Opened");

            var Open = (amountToDeposit >= MinimumBalance);
            if (Open)
            {
                this.accountId = accountId;
                CurrentBalance = amountToDeposit;
            }
        }

        public void Withdraw(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);

            if (amount <= 0)
                throw new Exception("Invalid Withdrawal Amount");

            var newBalance = CurrentBalance - amount;

            if (newBalance < MinimumBalance)
                throw new WithdrawalAmountTooLargeException(amount);

            CurrentBalance = newBalance;
        }
    }

    class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
        {
        }
        public AccountNotFoundException(long accountId)
            : base(String.Format("Account with Account ID: {0} not found", accountId))
        {
        }
    }

    class WithdrawalAmountTooLargeException : Exception
    {
        public WithdrawalAmountTooLargeException()
        {
        }
        public WithdrawalAmountTooLargeException(int amount)
            : base(String.Format("Amount too large to withdraw: {0}", amount))
        {
        }
    }
}
