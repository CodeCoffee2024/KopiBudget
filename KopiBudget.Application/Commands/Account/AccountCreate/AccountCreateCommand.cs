using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Account.AccountCreate
{
    public sealed record AccountCreateCommand(
        Guid UserId,
        Guid CategoryId,
        string Name,
        decimal Balance,
        bool IsExpense) : ICommand<AccountDto>;
}