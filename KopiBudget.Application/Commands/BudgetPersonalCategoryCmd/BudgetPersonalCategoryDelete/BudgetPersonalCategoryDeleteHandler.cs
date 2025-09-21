using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.BudgetPersonalCategoryCmd.BudgetPersonalCategoryDelete
{
    internal sealed class BudgetPersonalCategoryDeleteHandler(
        IBudgetPersonalCategoryRepository _repository,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<BudgetPersonalCategoryDeleteCommand>
    {
        #region Public Methods

        public async Task<Result> Handle(BudgetPersonalCategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.BudgetId) || string.IsNullOrEmpty(request.PersonalCategoryId))
            {
                return Result.Failure(Error.Notfound("Budget or Personal Transaction"));
            }
            var result = await _repository.GetByBudgetIdAndPersonalCategoryIdAsync(Guid.Parse(request.BudgetId), Guid.Parse(request.PersonalCategoryId));
            if (result is not null)
            {
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