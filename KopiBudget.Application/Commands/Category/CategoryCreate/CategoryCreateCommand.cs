using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Category.CategoryCreate
{
    public sealed record CategoryCreateCommand(
        Guid UserId,
        string Name) : ICommand<CategoryDto>;
}