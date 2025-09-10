namespace KopiBudget.Application.Dtos
{
    public class ExchangeRateRowDto
    {
        #region Properties

        public string Currency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal? PreviousRate { get; set; }
        public decimal? Change { get; set; }

        #endregion Properties
    }

    public class ExchangeRateDto
    {
        #region Properties

        public string BaseCurrency { get; set; } = string.Empty;
        public List<ExchangeRateRowDto> ConversionRates { get; set; } = new();

        #endregion Properties
    }
}