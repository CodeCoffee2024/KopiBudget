using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Auth.Login
{
    public sealed record AuthLoginCommand(string usernameEmail, string password) : ICommand<AuthDto>;
}