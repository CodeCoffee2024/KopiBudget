using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetCurrencyList
{
    public class GetCurrencyListQueryHandler(
        ISystemSettingsService _service
    ) : IRequestHandler<GetCurrencyListQuery, Result<IEnumerable<CurrencyDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<CurrencyDto>>> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken)
        {
            var result = _service.GetAllCurrencies();

            return Result.Success(result);
        }

        #endregion Public Methods
    }
}