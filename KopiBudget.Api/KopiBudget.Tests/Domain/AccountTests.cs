using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Tests.Domain
{
    public class AccountTests
    {
        #region Public Methods

        [Fact]
        public void Create_ShouldThrow_WhenBalanceIsNegative()
        {
            var user = Account.Create("username", -100, null, null);

            Assert.Throws<NegativeAmountException>(() => user);
        }

        #endregion Public Methods
    }
}