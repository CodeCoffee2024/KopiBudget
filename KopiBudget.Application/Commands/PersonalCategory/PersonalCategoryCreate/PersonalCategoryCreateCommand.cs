using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryCreate
{
    public sealed record PersonalCategoryCreateCommand(
        Guid UserId,
        string Name,
        string Color,
        string Icon) : ICommand<PersonalCategoryDto>;
}