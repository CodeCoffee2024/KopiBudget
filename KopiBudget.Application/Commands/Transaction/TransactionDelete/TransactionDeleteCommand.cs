using KopiBudget.Application.Abstractions.Messaging;

namespace KopiBudget.Application.Commands.Transaction.TransactionDelete
{
    public sealed record TransactionDeleteCommand(
        string? Id) : ICommand;
}