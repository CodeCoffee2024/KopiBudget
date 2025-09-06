using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Account.GetAccounts
{
    public class GetUserAccountQuery : IRequest<Result<IEnumerable<AccountDto>>>
    {
        #region Public Constructors

        public GetUserAccountQuery(Guid userId)
        {
            UserId = userId;
        }

        #endregion Public Constructors

        #region Properties

        public Guid UserId { get; set; }

        #endregion Properties
    }
}