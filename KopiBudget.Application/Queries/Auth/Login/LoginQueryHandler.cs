using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Auth.Login
{
    public class LoginQueryHandler(
        IUserRepository _repository,
        IJwtTokenGenerator _jwtTokenGenerator,
        IPasswordHasherService _passwordHasherService
    ) : IRequestHandler<LoginQuery, Result<AuthDto>>
    {
        #region Public Methods

        public async Task<Result<AuthDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.EmailUsernameExists(request.UsernameEmail!);

            if (request.UsernameEmail == null)
            {
                return Result.Failure<AuthDto>(Error.FormControl("usernameEmail", "This field is required"));
            }
            if (user == null)
            {
                return Result.Failure<AuthDto>(Error.FormControl("usernameEmail", "User not found"));
            }
            if (!_passwordHasherService.VerifyPassword(user!.Password, request.Password!))
            {
                return Result.Failure<AuthDto>(Error.FormControl("password", "Invalid password"));
            }
            var token = _jwtTokenGenerator.GenerateToken(user!);
            return Result.Success(token);
        }

        #endregion Public Methods
    }
}