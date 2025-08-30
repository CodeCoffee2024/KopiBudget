using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Commands.Auth.AuthRefreshToken
{
    public sealed record AuthRefreshTokenCommand(string refreshToken) : ICommand<AuthDto>;
}