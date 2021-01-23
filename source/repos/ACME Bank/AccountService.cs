using System;
namespace com.acme.test01.OlasunkanmiOtokiti
{
    public interface IAccount
    {
        void Deposit(long accountId, int amount);
        void Withdraw(long accountId, int amount);
        void OpenAccount(long accountId, int amountToDeposit);
    }
    public class CurrentAccount : IAccount
    {
        public int CurrentBalance = 0;
        private const int OverDraftLimit = -100000;
        private bool Opened = false;
        public long accountId;

        public void Deposit(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);
            if (amount <= 0)
                throw new Exception("Invalid deposit amount");
            CurrentBalance = CurrentBalance + amount;
        }
        public void Withdraw(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);
            if (amount <= 0)
                throw new Exception("Invalid withdrawal amount");
            var newBalance = CurrentBalance - amount;
            if (newBalance >= OverDraftLimit)
            {
                CurrentBalance = newBalance;
            }
            else
            {
                throw new WithdrawalTooLargeException(amount);
            }
        }
        public void OpenAccount(long accountId, int amountToDeposit)
        {
            if (this.accountId != 0)
                throw new Exception("Account Already Opened");
            if (amountToDeposit < 0)
                throw new Exception("Account Cannot be Opened");
            this.accountId = accountId;
            CurrentBalance = amountToDeposit;
            Opened = true;
        }
    }
    public class SavingsAccount : IAccount
    {
        public long accountId = 0;
        public int CurrentBalance;
        private const int MinimumBalance = 1000;

        public void OpenAccount(long accountId, int amountToDeposit)
        {
            if (this.accountId != 0)
                throw new Exception("Account Already Opened");
            var opened = amountToDeposit >= MinimumBalance;
            if (opened)
            {
                this.accountId = accountId;
                CurrentBalance = amountToDeposit;
            }
        }
        public void Deposit(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);
            if (amount <= 0)
                throw new Exception("Invalid deposit amount");
            CurrentBalance += amount;
        }

        public void Withdraw(long accountId, int amount)
        {
            if (this.accountId != accountId)
                throw new AccountNotFoundException(accountId);
            if (amount <= 0)
                throw new Exception("Invalid Withdrawal amount");
            var newBalance = CurrentBalance - amount;
            if (newBalance < MinimumBalance)
            {
                throw new WithdrawalTooLargeException(amount);
            }
            CurrentBalance = newBalance;
        }
    }
    class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
        {
        }
        public AccountNotFoundException(long accountId)
            : base(String.Format("Account with AccountId : {0} not Found", accountId))
        {
        }
    }
    class WithdrawalTooLargeException : Exception
    {
        public WithdrawalTooLargeException()
        {
        }
        public WithdrawalTooLargeException(int amount)
            : base(String.Format("Amount too large to withdraw: {0}", amount))
        {
        }
    }

}
