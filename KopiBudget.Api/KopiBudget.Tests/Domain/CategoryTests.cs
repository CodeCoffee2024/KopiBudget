using KopiBudget.Domain.Entities;

namespace KopiBudget.Tests.Domain
{
    public class CategoryTests
    {
        #region Public Methods

        [Fact]
        public void Create_ShouldSucceed_UponCreation()
        {
            var name = "Name";
            var entity = Category.Create(name, false, null, null);

            Assert.Equal(name, entity.Name);
        }

        #endregion Public Methods
    }
}