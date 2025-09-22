using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryUpdate
{
    public sealed record PersonalCategoryUpdateCommand(
        Guid UserId,
        string? Id,
        string? Name,
        string? Color,
        string? Icon) : ICommand<PersonalCategoryDto>;
}