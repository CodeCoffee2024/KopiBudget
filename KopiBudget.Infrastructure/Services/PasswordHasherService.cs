using KopiBudget.Application.Interfaces.Common;
using Microsoft.AspNetCore.Identity;

namespace KopiBudget.Infrastructure.Services
{
    public class PasswordHasherService() : IPasswordHasherService
    {
        #region Fields

        private readonly PasswordHasher<object> _passwordHasher = new();

        #endregion Fields

        #region Public Methods

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        #endregion Public Methods
    }
}