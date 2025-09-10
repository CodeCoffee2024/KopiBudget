using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetCurrencyDropdown
{
    public class GetCurrencyDropdownQueryHandler(
        ISystemSettingsService _service,
        IMapper _mapper
    ) : IRequestHandler<GetCurrencyDropdownQuery, Result<PageResult<CurrencyDto>>>
    {
        #region Public Methods

        public async Task<Result<PageResult<CurrencyDto>>> Handle(GetCurrencyDropdownQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = _service.GetAllCurrencies().Where(it => it.Code.Contains(request.Search!.ToUpper())).Take(10).Skip(request.PageNumber - 1 * 10);

            return Result.Success(
                new PageResult<CurrencyDto>(
                    _mapper.Map<IReadOnlyList<CurrencyDto>>(pagedResult),
                    pagedResult.Count(),
                    request.PageNumber,
                    10,
                    "Code")
                );
        }

        #endregion Public Methods
    }
}