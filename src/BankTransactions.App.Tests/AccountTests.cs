using Moneybox.App.Domain;
using Moneybox.App.Domain.Services;
using Moq;
using System;
using Xunit;

namespace Moneybox.App.Tests
{
    public class AccountTests
    {
        private readonly Account _fromAccount;
        private readonly Account _toAccount;
        private readonly INotificationService _notificationService;


        public AccountTests()
        {
            _fromAccount = new Account
            {
                Id = new Guid(),
                User = new User
                {
                    Id = new Guid(),
                    Name = "Test FromAccount",
                    Email = "fromaccount.test@provider.com"
                }

            };

            _toAccount = new Account
            {
                Id = new Guid(),
                User = new User
                {
                    Id = new Guid(),
                    Name = "Mr Test ToAccount",
                    Email = "toaccount.test@provider.com"
                }
            };

            _notificationService = new Mock<INotificationService>().Object;
        }

        [Fact]
        public void Test1()
        {
            _fromAccount.TransferMoney(_toAccount, 500m, _notificationService);
        }
    }
}
