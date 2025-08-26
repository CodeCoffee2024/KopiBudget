using FluentAssertions;
using KopiBudget.Infrastructure.Services;

namespace KopiBudget.Tests.Application
{
    public class PasswordHasherServiceTests
    {
        #region Fields

        private readonly PasswordHasherService _sut;

        #endregion Fields

        #region Public Constructors

        public PasswordHasherServiceTests()
        {
            _sut = new PasswordHasherService();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void HashPassword_ShouldReturnHashedValue()
        {
            var result = _sut.HashPassword("secret123");

            result.Should().NotBeNullOrEmpty();
            result.Should().NotBe("secret123"); // should not equal original
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
        {
            var hash = _sut.HashPassword("secret123");
            var result = _sut.VerifyPassword(hash, "secret123");

            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForWrongPassword()
        {
            var hash = _sut.HashPassword("secret123");

            var result = _sut.VerifyPassword(hash, "wrong");

            result.Should().BeFalse();
        }

        #endregion Public Methods
    }
}