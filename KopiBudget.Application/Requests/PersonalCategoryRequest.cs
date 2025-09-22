using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryCreate;
using KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryUpdate;
using KopiBudget.Application.Queries.PersonalCategory.PersonalCategoryDropdown;

namespace KopiBudget.Application.Requests
{
    public class PersonalCategoryRequest : PageRequest
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Icon { get; set; } = string.Empty;
        public string? Color { get; set; } = string.Empty;
        public string BudgetIds { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public PersonalCategoryCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, Name!, Color!, Icon!);

        public PersonalCategoryUpdateCommand SetUpdateCommand(Guid UserId, string Id) =>
            new(UserId, Id, Name!, Color!, Icon!);

        //public GetCategoryListQuery ToQuery() => new(Search, OrderBy, PageNumber, PageSize, Status);

        public PersonalCategoryDropdownQuery ToDropdownQuery(Guid UserId) => new(UserId, BudgetId, Search!, PageNumber, Exclude, BudgetIds);

        #endregion Public Methods
    }
}