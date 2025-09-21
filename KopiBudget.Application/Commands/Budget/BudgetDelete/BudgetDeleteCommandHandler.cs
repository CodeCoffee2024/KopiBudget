using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Budget.BudgetDelete
{
    internal sealed class BudgetDeleteCommandHandler(
        IBudgetRepository _repository,
        //IAccountRepository _accountRepository,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<BudgetDeleteCommand>
    {
        #region Public Methods

        public async Task<Result> Handle(BudgetDeleteCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Result.Failure(Error.Notfound("Budget"));
            }
            var result = await _repository.GetByIdAsync(Guid.Parse(request.Id));
            if (result is not null)
            {
                //var account = await _accountRepository.GetByIdAsync(result!.AccountId!.Value);
                //account!.AddToBalance(result.Amount);
                _repository.Remove(result);
            }
            else
            {
                return Result.Failure(Error.Notfound("Transaction"));
            }
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        #endregion Public Methods
    }
}