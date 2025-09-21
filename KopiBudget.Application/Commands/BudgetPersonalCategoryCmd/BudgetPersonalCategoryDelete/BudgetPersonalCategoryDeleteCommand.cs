using KopiBudget.Application.Abstractions.Messaging;

namespace KopiBudget.Application.Commands.BudgetPersonalCategoryCmd.BudgetPersonalCategoryDelete
{
    public sealed record BudgetPersonalCategoryDeleteCommand(
        string? BudgetId, string? PersonalCategoryId) : ICommand;
}