using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.Budget.BudgetCreate;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Queries.Budget.BudgetDropdown;

namespace KopiBudget.Application.Requests
{
    public class BudgetRequest : PageRequest
    {
        #region Properties

        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Amount { get; set; } = string.Empty;
        public string? StartDate { get; set; } = string.Empty;
        public string? EndDate { get; set; } = string.Empty;
        public IEnumerable<BudgetPersonalCategoryDto>? BudgetPersonalCategories { get; set; } = new List<BudgetPersonalCategoryDto>();

        #endregion Properties

        #region Public Methods

        public BudgetCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, Name, Amount, StartDate, EndDate, BudgetPersonalCategories);

        public BudgetDropdownQuery ToDropdownQuery() => new(Search!, PageNumber, Exclude);

        #endregion Public Methods
    }
}