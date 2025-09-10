using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.ExchangeRate.GetExchangeRates
{
    public class GetExchangeRatesQueryHandler(IExchangeRateProviderService _provider, IMapper _mapper, ISystemSettingsRepository _repository) : IRequestHandler<GetExchangeRatesQuery, Result<ExchangeRateDto>>
    {
        #region Public Methods

        public async Task<Result<ExchangeRateDto>> Handle(GetExchangeRatesQuery request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetSettingsAsync();
            try
            {
                var exchangeRate = await _provider.GetLatestRatesAsync(currency!.Currency);
                return _mapper.Map<ExchangeRateDto>(exchangeRate);
            }
            catch (Exception)
            {
                var exchangeRate = await _provider.GetLatestRatesAsync(currency!.Currency);
                return _mapper.Map<ExchangeRateDto>(exchangeRate);
            }
        }

        #endregion Public Methods
    }
}