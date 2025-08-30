using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Application.Commands.Auth.AuthRefreshToken
{
    public class AuthRefreshTokenCommandHandler(
        IJwtTokenGenerator _jwtTokenGenerator
    ) : ICommandHandler<AuthRefreshTokenCommand, AuthDto>
    {
        #region Public Methods

        public async Task<Result<AuthDto>> Handle(AuthRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var authDto = await _jwtTokenGenerator.RefreshToken(command.refreshToken);

            if (authDto is null)
                return Result.Failure<AuthDto>(Error.InvalidRequest);

            return Result.Success(authDto);
        }

        #endregion Public Methods
    }
}