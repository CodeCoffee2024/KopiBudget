namespace KopiBudget.Common.Entities
{
    public class JwtSettings
    {
        #region Properties

        public string Key { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpirationMinutes { get; set; }

        #endregion Properties
    }
}