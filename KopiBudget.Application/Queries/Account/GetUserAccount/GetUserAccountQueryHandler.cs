using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Queries.Account.GetAccounts;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Account.GetUserAccount
{
    public class GetUserAccountQueryHandler(
        IAccountRepository _accountRepository,
        IMapper _mapper
    ) : IRequestHandler<GetUserAccountQuery, Result<IEnumerable<AccountDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<AccountDto>>> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetAllByUserIdAsync(request.UserId);

            return Result.Success(_mapper.Map<IEnumerable<AccountDto>>(result));
        }

        #endregion Public Methods
    }
}