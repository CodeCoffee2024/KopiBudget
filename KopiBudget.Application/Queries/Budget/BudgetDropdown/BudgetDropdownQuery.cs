using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Budget.BudgetDropdown
{
    public class BudgetDropdownQuery : IRequest<Result<PageResult<BudgetDropdownDto>>>
    {
        #region Public Constructors

        public BudgetDropdownQuery(string search, int pageNumber = 1, string? exclude = "", Guid? userId = null)
        {
            Search = search;
            PageNumber = pageNumber;
            Exclude = exclude!;
            UserId = userId;
        }

        #endregion Public Constructors

        #region Properties

        public string Search { get; set; }
        public string Exclude { get; set; }
        public int PageNumber { get; set; }
        public Guid? UserId { get; set; }

        #endregion Properties
    }
}