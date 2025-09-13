using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Account.AccountDelete
{
    internal sealed class AccountDeleteCommandHandler(
        IAccountRepository _repository,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<AccountDeleteCommand>
    {
        #region Public Methods

        public async Task<Result> Handle(AccountDeleteCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Result.Failure(Error.Notfound("Account"));
            }
            var result = await _repository.GetByIdAsync(Guid.Parse(request.Id));
            if (result is not null)
            {
                _repository.Remove(result);
                await _unitOfWork.SaveChangesAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(Error.Notfound("Account"));
            }
        }

        #endregion Public Methods
    }
}