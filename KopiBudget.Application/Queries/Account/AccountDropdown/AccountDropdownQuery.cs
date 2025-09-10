using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Account.AccountDropdown
{
    public class AccountDropdownQuery : IRequest<Result<PageResult<AccountDropdownDto>>>
    {
        #region Public Constructors

        public AccountDropdownQuery(string search, int pageNumber = 1, string? exclude = "")
        {
            Search = search;
            Exclude = exclude;
            PageNumber = pageNumber;
        }

        #endregion Public Constructors

        #region Properties

        public string Search { get; set; }
        public string? Exclude { get; set; } = string.Empty;
        public int PageNumber { get; set; }

        #endregion Properties
    }
}