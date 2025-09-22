using KopiBudget.Application.Abstractions.Messaging;

namespace KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryDelete
{
    public sealed record PersonalCategoryDeleteCommand(
        string? Id) : ICommand;
}