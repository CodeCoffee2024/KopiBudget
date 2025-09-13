using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Transaction.TransactionUpdate
{
    public sealed record TransactionUpdateCommand(
        Guid UserId,
        string? Id,
        string? CategoryId,
        string? AccountId,
        string? Date,
        string? Time,
        string? Amount,
        string Note,
        bool? InputTime) : ICommand<TransactionDto>;
}