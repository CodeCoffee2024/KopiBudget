using KopiBudget.Application.Abstractions.Messaging;

namespace KopiBudget.Application.Commands.Account.AccountDelete
{
    public sealed record AccountDeleteCommand(
        string? Id) : ICommand;
}