using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetCurrencyDropdown
{
    public class GetCurrencyDropdownQuery : IRequest<Result<PageResult<CurrencyDto>>>
    {
        #region Public Constructors

        public GetCurrencyDropdownQuery(string search, int pageNumber = 1)
        {
            Search = search;
            PageNumber = pageNumber;
        }

        #endregion Public Constructors

        #region Properties

        public string Search { get; set; }
        public int PageNumber { get; set; }

        #endregion Properties
    }
}