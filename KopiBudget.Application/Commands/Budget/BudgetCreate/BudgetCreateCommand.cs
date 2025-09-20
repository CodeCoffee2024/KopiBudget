using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Budget.BudgetCreate
{
    public sealed record BudgetCreateCommand(
        Guid UserId,
        string? Name,
        string? Amount,
        string? StartDate,
        string? EndDate,
        IEnumerable<BudgetPersonalCategoryDto>? BudgetPersonalCategories) : ICommand<BudgetDto>;
}