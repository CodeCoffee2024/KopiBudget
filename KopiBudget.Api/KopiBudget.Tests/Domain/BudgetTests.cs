using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Tests.Domain
{
    public class BudgetTests
    {
        #region Public Methods

        [Fact]
        public void Constructor_Should_SetNameAndAmount()
        {
            // Arrange
            var name = "Food Budget";
            var amount = 1000m;
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now;

            // Act
            var budget = Budget.Create(amount, name, startDate, endDate, null, null, null, null);

            // Assert
            Assert.Equal(name, budget.Name);
            Assert.Equal(amount, budget.Amount);
            Assert.Equal(startDate, budget.StartDate);
            Assert.Equal(endDate, budget.EndDate);
        }

        [Fact]
        public void Constructor_Should_Throw_WhenAmountIsNegative()
        {
            // Arrange
            var name = "Invalid Budget";
            var amount = -100;
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now;

            // Act & Assert
            var entity = Budget.Create(amount, name, startDate, endDate, null, null, null, null);
            Assert.Throws<InvalidDateRangeException>(() => entity);
        }

        [Fact]
        public void UpdateAmount_Should_UpdateFields()
        {
            var name = "Invalid Budget";
            var amount = -100;
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now;
            // Arrange
            var budget = Budget.Create(amount, name, startDate, endDate, null, null, null, null);

            // Act
            budget.Update(750, "Update Name", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), null, null, null, null);

            // Assert
            Assert.Equal(750, budget.Amount);
            Assert.Equal("Update Name", budget.Name);
            Assert.Equal(DateTime.Now.AddDays(-2), budget.StartDate);
            Assert.Equal(DateTime.Now.AddDays(-1), budget.EndDate);
        }

        [Fact]
        public void UpdateAmount_Should_Throw_WhenNegative()
        {
            // Arrange

            var amount = -100;

            var entity = Budget.Create(amount, "Invalid", DateTime.Now, DateTime.Now.AddDays(30), null, null, null, null);
            // Act & Assert
            Assert.Throws<InvalidDateRangeException>(() =>
                entity.Update(-10, "Invalid", DateTime.Now, DateTime.Now.AddDays(30), null, null, null, null));
        }

        #endregion Public Methods
    }
}