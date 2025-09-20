using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Queries.PersonalCategory.PersonalCategoryDropdown;

namespace KopiBudget.Application.Requests
{
    public class PersonalCategoryRequest : PageRequest
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BudgetIds { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        //public GetCategoryListQuery ToQuery() => new(Search, OrderBy, PageNumber, PageSize, Status);

        public PersonalCategoryDropdownQuery ToDropdownQuery(Guid UserId) => new(UserId, BudgetId, Search!, PageNumber, Exclude, BudgetIds);

        #endregion Public Methods
    }
}