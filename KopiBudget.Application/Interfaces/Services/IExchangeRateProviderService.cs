using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Interfaces.Services
{
    public interface IExchangeRateProviderService
    {
        #region Public Methods

        Task<ExchangeRateDto?> GetLatestRatesAsync(string? currency);

        #endregion Public Methods
    }
}