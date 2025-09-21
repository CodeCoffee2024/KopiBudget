using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.BudgetPersonalCategoryCmd.BudgetPersonalCategoryUpdate;

namespace KopiBudget.Application.Requests
{
    public class BudgetPersonalCategoryRequest : PageRequest
    {
        #region Properties

        public Guid Id { get; set; }
        public string? BudgetId { get; set; } = string.Empty;
        public string? PersonalCategoryId { get; set; } = string.Empty;
        public string? Limit { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public BudgetPersonalCategoryUpdateCommand SetUpdateCommand(Guid UserId, string BudgetId, string PersonalCategoryId) =>
             new(UserId, BudgetId, PersonalCategoryId, Limit);

        #endregion Public Methods
    }
}