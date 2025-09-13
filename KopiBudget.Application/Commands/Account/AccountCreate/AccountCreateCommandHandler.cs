using AutoMapper;
using FluentValidation;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Account.AccountCreate
{
    internal sealed class AccountCreateCommandHandler(
        IAccountRepository _repository,
        IValidator<AccountCreateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<AccountCreateCommand, AccountDto>
    {
        #region Public Methods

        public async Task<Result<AccountDto>> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<AccountDto>(Error.Validation, validationResult.ToErrorList());
            }

            var entity = KopiBudget.Domain.Entities.Account.Create(request.Name, request.Balance, request.IsDebt, request.UserId, Guid.Parse(request.CategoryId!), request.UserId, DateTime.UtcNow);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<AccountDto>(entity));
        }

        #endregion Public Methods
    }
}