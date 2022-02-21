using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Domain
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public (Account, Account) TransferMoney(Account toAccount, decimal amount, INotificationService notificationService)
        {
            WithdrawMoneyFromThis(amount, notificationService);

            var paidIn = toAccount.PaidIn + amount;

            if (paidIn > PayInLimit)
                throw new InvalidOperationException("Account pay in limit reached.");

            if (PayInLimit - paidIn < 500m)
                notificationService.NotifyApproachingPayInLimit(toAccount.User.Email);

            toAccount.Balance += amount;
            toAccount.PaidIn = paidIn;

            return (this, toAccount);
        }

        public Account WithdrawMoneyFromThis(decimal amount, INotificationService notificationService)
        {
            var fromBalance = Balance - amount;

            switch (fromBalance)
            {
                case < 0m:
                    throw new InvalidOperationException("Insufficient funds.");
                case < 500m:
                    notificationService.NotifyFundsLow(User.Email);
                    break;
            }

            Balance = fromBalance;
            Withdrawn += amount;

            return this;
        }
    }
}
