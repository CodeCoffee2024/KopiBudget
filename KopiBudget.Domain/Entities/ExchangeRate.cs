namespace KopiBudget.Domain.Entities
{
    public class ExchangeRate
    {
        #region Properties

        public string BaseCurrency { get; set; } = default!;
        public DateTime RetrievedAt { get; set; }
        public Dictionary<string, decimal> ConversionRates { get; set; } = new();

        #endregion Properties
    }
}