using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.BudgetPersonalCategoryCmd.BudgetPersonalCategoryUpdate
{
    public sealed record BudgetPersonalCategoryUpdateCommand(
        Guid UserId,
        string? BudgetId,
        string? PersonalCategoryId,
        string? Limit) : ICommand<BudgetPersonalCategoryDto>;
}