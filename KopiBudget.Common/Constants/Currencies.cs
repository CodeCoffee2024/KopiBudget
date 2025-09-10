namespace KopiBudget.Common.Constants
{
    public static class Currencies
    {
        #region Fields

        public const string USD = "USD";
        public const string EUR = "EUR";
        public const string GBP = "GBP";
        public const string JPY = "JPY";
        public const string AUD = "AUD";
        public const string CAD = "CAD";
        public const string CHF = "CHF";
        public const string CNY = "CNY";
        public const string PHP = "PHP";

        public static readonly Dictionary<string, string> LIST = new()
        {
            { USD, "United States Dollar" },
            { EUR, "Euro" },
            { GBP, "British Pound Sterling" },
            { JPY, "Japanese Yen" },
            { AUD, "Australian Dollar" },
            { CAD, "Canadian Dollar" },
            { CHF, "Swiss Franc" },
            { CNY, "Chinese Yuan" },
            { PHP, "Philippine Peso" }
        };

        #endregion Fields
    }
}