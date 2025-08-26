using KopiBudget.Application.Queries.Auth;

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
        public DateTime? ExpiresAt { get; set; }

        #endregion Properties

        #region Public Methods

        public LoginQuery LoginQuery() => new(UsernameEmail!, Password!);

        #endregion Public Methods
    }
}