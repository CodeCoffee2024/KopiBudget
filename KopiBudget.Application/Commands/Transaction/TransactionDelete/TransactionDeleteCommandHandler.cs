using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Transaction.TransactionDelete
{
    internal sealed class TransactionDeleteCommandHandler(
        ITransactionRepository _repository,
        IAccountRepository _accountRepository,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<TransactionDeleteCommand>
    {
        #region Public Methods

        public async Task<Result> Handle(TransactionDeleteCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Result.Failure(Error.Notfound("Transaction"));
            }
            var result = await _repository.GetByIdAsync(Guid.Parse(request.Id));
            if (result is not null)
            {
                var account = await _accountRepository.GetByIdAsync(result!.AccountId!.Value);
                account!.AddToBalance(result.Amount);
                _repository.Remove(result);
                await _unitOfWork.SaveChangesAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(Error.Notfound("Transaction"));
            }
        }

        #endregion Public Methods
    }
}