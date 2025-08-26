using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Auth
{
    public class LoginQuery : IRequest<Result<AuthDto>>
    {
        #region Public Constructors

        public LoginQuery(string usernameEmail, string password)
        {
            UsernameEmail = usernameEmail;
            Password = password;
        }

        public string? UsernameEmail { get; set; }
        public string? Password { get; set; }

        #endregion Public Constructors
    }
}