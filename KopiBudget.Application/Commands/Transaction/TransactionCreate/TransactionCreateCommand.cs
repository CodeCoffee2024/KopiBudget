using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Transaction.TransactionCreate
{
    public sealed record TransactionCreateCommand(
        Guid UserId,
        Guid CategoryId,
        Guid AccountId,
        DateTime Date,
        string? Time,
        decimal Amount,
        string Note,
        bool? InputTime) : ICommand<TransactionDto>;
}