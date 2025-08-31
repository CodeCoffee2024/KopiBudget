using KopiBudget.Application.Commands.Auth.AuthRefreshToken;
using KopiBudget.Application.Commands.Auth.Login;

namespace KopiBudget.Application.Requests
{
    public class AuthRequest
    {
        #region Properties

        public string? Email { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? Password { get; set; } = default!;
        public string? UsernameEmail { get; set; } = default!;
        public string? Token { get; set; } = default!;
        public string? RefreshToken { get; set; } = default!;
        public DateTime? ExpiresAt { get; set; }

        #endregion Properties

        #region Public Methods

        public AuthLoginCommand LoginQuery() => new(UsernameEmail!, Password!);

        public AuthRefreshTokenCommand SetRefreshToken() => new(RefreshToken!);

        #endregion Public Methods
    }
}