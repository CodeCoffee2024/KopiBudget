using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Commands.User.UserRegister
{
    public sealed record UserRegisterCommand(
        string UserName,
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string MiddleName) : IRequest<Result>;
}