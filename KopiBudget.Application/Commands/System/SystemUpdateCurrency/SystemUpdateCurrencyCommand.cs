using KopiBudget.Application.Abstractions.Messaging;

namespace KopiBudget.Application.Commands.System.SystemUpdateCurrency
{
    public sealed record SystemUpdateCurrencyCommand(
        Guid UserId,
        string Currency) : ICommand;
}