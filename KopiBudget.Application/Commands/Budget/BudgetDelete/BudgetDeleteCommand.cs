using KopiBudget.Application.Abstractions.Messaging;

namespace KopiBudget.Application.Commands.Budget.BudgetDelete
{
    public sealed record BudgetDeleteCommand(
        string? Id) : ICommand;
}