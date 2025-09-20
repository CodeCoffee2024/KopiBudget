using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.PersonalCategory.PersonalCategoryDropdown
{
    public class PersonalCategoryDropdownQuery : IRequest<Result<PageResult<PersonalCategoryDropdownDto>>>
    {
        #region Public Constructors

        public PersonalCategoryDropdownQuery(Guid userId, Guid budgetId, string search, int pageNumber = 1, string? exclude = "", string? budgetIds = "")
        {
            UserId = userId;
            BudgetId = budgetId;
            Search = search;
            PageNumber = pageNumber;
            Exclude = exclude!;
            BudgetIds = budgetIds!;
        }

        #endregion Public Constructors

        #region Properties

        public string Search { get; set; }
        public Guid UserId { get; set; }
        public Guid BudgetId { get; set; }
        public string BudgetIds { get; set; }
        public string Exclude { get; set; }
        public int PageNumber { get; set; }

        #endregion Properties
    }
}