namespace KopiBudget.Application.Dtos
{
    public class AuthDto
    {
        #region Properties

        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }

        #endregion Properties
    }
}