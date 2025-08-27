using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Tests.Domain
{
    public class UserTests
    {
        #region Public Methods

        [Fact]
        public void Create_ShouldThrow_WhenEmailIsEmpty()
        {
            var user = User.Create("username", "", "test", "status", "firstName", "lastName", "", null, null, true);

            Assert.Throws<InvalidEmailException>(() => user);
        }

        [Fact]
        public void Create_ShouldThrow_WhenEmailIsInvalid()
        {
            var user = User.Create("username", "test@.com", "test", "status", "firstName", "lastName", "", null, null, true);

            Assert.Throws<InvalidEmailException>(() => user);
        }

        [Fact]
        public void Create_ShouldSucceed_WhenEmailIsValid()
        {
            var user = User.Create("username", "test@example.com", "test", "status", "firstName", "lastName", "", null, null, true);

            Assert.Equal("test@example.com", user.Email);
        }

        [Fact]
        public void Register_ShouldSucceed_WhenEmailIsValid()
        {
            var user = User.Register("username", "test@example.com", "status", "firstName", "lastName", "");

            Assert.Equal("test@example.com", user.Email);
        }

        #endregion Public Methods
    }
}