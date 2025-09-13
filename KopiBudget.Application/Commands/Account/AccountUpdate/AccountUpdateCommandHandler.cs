using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Account.AccountUpdate
{
    internal sealed class AccountUpdateCommandHandler(
        IAccountRepository _repository,
        ICategoryRepository _categoryRepository,
        IValidator<AccountUpdateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<AccountUpdateCommand, AccountDto>
    {
        #region Public Methods

        public async Task<Result<AccountDto>> Handle(AccountUpdateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<AccountDto>(Error.Validation, validationResult.ToErrorList());
            }
            var account = await _repository.GetByIdAsync(Guid.Parse(request.Id!));
            var category = await _categoryRepository.GetByIdAsync(Guid.Parse(request.CategoryId!));
            if (category == null)
            {
                validationResult.Errors.Add(new ValidationFailure("CategoryId", "Category not found"));
            }
            if (account == null)
            {
                validationResult.Errors.Add(new ValidationFailure("Account", "Account not found"));
            }
            if (!validationResult.IsValid)
            {
                return Result.Failure<AccountDto>(Error.Validation, validationResult.ToErrorList());
            }

            account!.Update(request.Name, request.Balance, request.IsDebt, Guid.Parse(request.CategoryId!), request.UserId, DateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<AccountDto>(account));
        }

        #endregion Public Methods
    }
}