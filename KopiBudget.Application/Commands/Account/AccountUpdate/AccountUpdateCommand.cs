using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Account.AccountUpdate
{
    public sealed record AccountUpdateCommand(
        Guid UserId,
        string? Id,
        string? CategoryId,
        string Name,
        decimal Balance,
        bool IsDebt) : ICommand<AccountDto>;
}