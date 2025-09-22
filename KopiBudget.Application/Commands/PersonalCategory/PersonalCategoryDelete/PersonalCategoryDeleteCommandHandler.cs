using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryDelete
{
    internal sealed class PersonalCategoryDeleteCommandHandler(
        IPersonalCategoryRepository _repository,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<PersonalCategoryDeleteCommand>
    {
        #region Public Methods

        public async Task<Result> Handle(PersonalCategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Result.Failure(Error.Notfound("Personal Category"));
            }
            var result = await _repository.GetByIdAsync(Guid.Parse(request.Id));
            if (result is not null)
            {
                _repository.Remove(result);
            }
            else
            {
                return Result.Failure(Error.Notfound("Personal Category"));
            }
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        #endregion Public Methods
    }
}