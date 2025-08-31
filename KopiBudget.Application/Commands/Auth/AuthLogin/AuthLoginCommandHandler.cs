using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Auth.Login
{
    public class AuthLoginCommandHandler(
        IUserRepository _repository,
        IJwtTokenGenerator _jwtTokenGenerator,
        IPasswordHasherService _passwordHasherService
    ) : ICommandHandler<AuthLoginCommand, AuthDto>
    {
        #region Public Methods

        public async Task<Result<AuthDto>> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.EmailUsernameExists(request.usernameEmail!);

            if (request.usernameEmail == null)
            {
                return Result.Failure<AuthDto>(Error.FormControl("usernameEmail", "This field is required"));
            }
            if (user == null)
            {
                return Result.Failure<AuthDto>(Error.FormControl("usernameEmail", "User not found"));
            }
            if (!_passwordHasherService.VerifyPassword(user!.Password, request.password!))
            {
                return Result.Failure<AuthDto>(Error.FormControl("password", "Invalid password"));
            }
            var token = _jwtTokenGenerator.GenerateToken(user!);
            return Result.Success(token);
        }

        #endregion Public Methods
    }
}