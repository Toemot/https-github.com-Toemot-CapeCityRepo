using NUnit.Framework;
using PresLes;
using System;


namespace ACME.UnitTests
{
    [TestFixture]
    public class CurrentAccountTest
    {
        public long accountId = 1234;

        [Test]
        public void Depoist_ConfirmAccountId_ThrowAccountNotFoundException()
        {
            var account = new CurrentAccount();

            Assert.That(() => account.OpenAcccount(accountId, -1), Throws.Exception);
        }
    }

    [TestFixture]
    public class SavingsAccountTest
    {
        private SavingsAccount _savings;

        [SetUp]
        public void SetUp() 
        {
            _savings = new SavingsAccount();
            _savings.accountId = 0;
            _savings.CurrentBalance = 1000;
        }
        [Test]
        public void OpenAccount_ConfirmAccountId_ThrowAccountNotFoundException()
        {
            var account = new SavingsAccount();

            account.OpenAcccount(0, 1000);

            Assert.That(account.accountId, Is.EqualTo(0));
            Assert.That(account.CurrentBalance, Is.EqualTo(1000));
            //Assert.That(() => account.OpenAcccount(accountId, 1000), Throws.Exception.TypeOf<System.Exception>());
        }

        [Test]
        public void OpenAccount_AccountIdNotEqualToZero_ReturnException()
        {
            var account = new SavingsAccount();

            Assert.That(() => account.OpenAcccount(0,100), Throws.Exception.TypeOf<System.Exception>());
        }
    }
}
