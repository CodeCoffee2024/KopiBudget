using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Auth.AuthUserDetail
{
    public class AuthUserDetailQuery : IRequest<Result<UserDetailDto>>
    {
        #region Public Constructors

        public AuthUserDetailQuery(Guid userId)
        {
            UserId = userId;
        }

        #endregion Public Constructors

        #region Properties

        public Guid UserId { get; }

        #endregion Properties
    }
}