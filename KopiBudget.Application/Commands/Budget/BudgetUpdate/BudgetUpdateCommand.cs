using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Budget.BudgetUpdate
{
    public sealed record BudgetUpdateCommand(
        Guid UserId,
        string? Id,
        string? Name,
        string? Amount,
        string? StartDate,
        string? EndDate,
        IEnumerable<BudgetPersonalCategoryDto>? BudgetPersonalCategories) : ICommand<BudgetDto>;
}