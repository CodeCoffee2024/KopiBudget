using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Transaction.TransactionCreate
{
    internal sealed class TransactionCreateCommandHandler(
        ITransactionRepository _repository,
        IAccountRepository _accountRepository,
        ICategoryRepository _categoryRepository,
        IBudgetRepository _budgetRepository,
        IBudgetPersonalCategoryRepository _budgetPersonalCategoryRepository,
        IValidator<TransactionCreateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<TransactionCreateCommand, TransactionDto>
    {
        #region Public Methods

        public async Task<Result<TransactionDto>> Handle(TransactionCreateCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<TransactionDto>(Error.Validation, validation.ToErrorList());

            if (decimal.Parse(request.Amount!) <= 0)
            {
                validation.Errors.Add(new ValidationFailure("Amount", "Amount must be greater than 0"));
            }
            if (request.Type == TransactionTypes.Account)
            {
                var account = await _accountRepository.GetByIdAsync(Guid.Parse(request.AccountId!));
                var category = await _categoryRepository.GetByIdAsync(Guid.Parse(request.CategoryId!));
                if (account == null)
                {
                    validation.Errors.Add(new ValidationFailure("AccountId", "Account not found"));
                }
                if (category == null)
                {
                    validation.Errors.Add(new ValidationFailure("CategoryId", "Category not found"));
                }
                if ((account!.Balance - decimal.Parse(request.Amount!)) < 0)
                {
                    validation.Errors.Add(new ValidationFailure("Amount", "Amount is greater than balance"));
                }
                if ((account!.Balance - decimal.Parse(request.Amount!)) < 0)
                {
                    validation.Errors.Add(new ValidationFailure("Amount", "Amount is greater than balance"));
                }
                if (!validation.IsValid)
                    return Result.Failure<TransactionDto>(Error.Validation, validation.ToErrorList());
                account!.UpdateBalance(decimal.Parse(request.Amount!));
                var entity = KopiBudget.Domain.Entities.Transaction.Create(
                    decimal.Parse(request.Amount!),
                    this.CombineDateAndTimeUtc(DateTime.Parse(request.Date!), request.Time),
                    Guid.Parse(request.CategoryId!),
                    Guid.Parse(request.AccountId!),
                    null,
                    null,
                    TransactionTypes.Account,
                    request.Note,
                    request.UserId,
                    DateTime.UtcNow
                );
                await _repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return Result.Success(_mapper.Map<TransactionDto>(entity));
            }
            else
            {
                var budget = await _budgetRepository.GetByIdAsync(Guid.Parse(request.BudgetId!));
                var budgetPersonalCategory = await _budgetPersonalCategoryRepository.GetByBudgetIdAndPersonalCategoryIdAsync(Guid.Parse(request.BudgetId!), Guid.Parse(request.PersonalCategoryId!));

                if (budget == null)
                {
                    validation.Errors.Add(new ValidationFailure("BudgetId", "Budget not found"));
                }
                if (budgetPersonalCategory == null)
                {
                    validation.Errors.Add(new ValidationFailure("PersonalCategoryId", "Personal Category not found"));
                }
                if (budgetPersonalCategory!.Limit < decimal.Parse(request.Amount!))
                {
                    validation.Errors.Add(new ValidationFailure("Amount", "Amount is greater than limit"));
                }
                if (budget!.StartDate > DateTime.Parse(request.Date!) || budget!.EndDate < DateTime.Parse(request.Date!))
                {
                    validation.Errors.Add(new ValidationFailure("Date", "Date does not meet the budget date period"));
                }
                if (!validation.IsValid)
                    return Result.Failure<TransactionDto>(Error.Validation, validation.ToErrorList());
                budgetPersonalCategory!.UpdateTransactionAmount(decimal.Parse(request.Amount!), true);
                var entity = KopiBudget.Domain.Entities.Transaction.Create(
                    decimal.Parse(request.Amount!),
                    this.CombineDateAndTimeUtc(DateTime.Parse(request.Date!), request.Time),
                    null,
                    null,
                    Guid.Parse(request.BudgetId!),
                    Guid.Parse(request.PersonalCategoryId!),
                    TransactionTypes.Budget,
                    request.Note,
                    request.UserId,
                    DateTime.UtcNow
                );
                await _repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return Result.Success(_mapper.Map<TransactionDto>(entity));
            }
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