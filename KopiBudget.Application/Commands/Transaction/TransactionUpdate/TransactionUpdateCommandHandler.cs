using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Transaction.TransactionUpdate
{
    internal sealed class TransactionUpdateCommandHandler(
        ITransactionRepository _repository,
        IAccountRepository _accountRepository,
        ICategoryRepository _categoryRepository,
        IValidator<TransactionUpdateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<TransactionUpdateCommand, TransactionDto>
    {
        #region Public Methods

        public async Task<Result<TransactionDto>> Handle(TransactionUpdateCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<TransactionDto>(Error.Validation, validation.ToErrorList());

            var transaction = await _repository.GetByIdAsync(Guid.Parse(request.Id!));
            var account = await _accountRepository.GetByIdAsync(Guid.Parse(request.AccountId!));
            var category = await _categoryRepository.GetByIdAsync(Guid.Parse(request.CategoryId!));
            if (transaction == null)
            {
                validation.Errors.Add(new ValidationFailure("Id", "Transaction not found"));
            }
            if (account == null)
            {
                validation.Errors.Add(new ValidationFailure("AccountId", "Account not found"));
            }
            if (category == null)
            {
                validation.Errors.Add(new ValidationFailure("CategoryId", "Category not found"));
            }
            if (decimal.Parse(request.Amount!) <= 0)
            {
                validation.Errors.Add(new ValidationFailure("Amount", "Amount must be greater than 0"));
            }

            account!.AddToBalance(transaction!.Amount); // reverts previous amount
            if ((account!.Balance - decimal.Parse(request.Amount!)) < 0)
            {
                validation.Errors.Add(new ValidationFailure("Amount", "Amount is greater than balance"));
            }
            if (!validation.IsValid)
                return Result.Failure<TransactionDto>(Error.Validation, validation.ToErrorList());
            account!.UpdateBalance(decimal.Parse(request.Amount!));
            transaction.Update(decimal.Parse(request.Amount!), this.CombineDateAndTimeUtc(DateTime.Parse(request.Date!), request.InputTime!.Value ? request.Time : string.Empty), Guid.Parse(request.CategoryId!), Guid.Parse(request.AccountId!), request.Note, request.UserId, DateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<TransactionDto>(transaction));
        }

        public DateTime CombineDateAndTimeUtc(DateTime? date, string? time)
        {
            if (date == null)
                throw new ArgumentNullException(nameof(date), "Date is required.");

            if (string.IsNullOrWhiteSpace(time))
                time = "00:00";

            if (!TimeSpan.TryParse(time, out var timeSpan))
                throw new FormatException($"Invalid time format: {time}");

            var localDateTime = date.Value.Date.Add(timeSpan);

            return DateTime.SpecifyKind(localDateTime, DateTimeKind.Local).ToUniversalTime();
        }

        #endregion Public Methods
    }
}