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

namespace KopiBudget.Application.Commands.Budget.BudgetCreate
{
    internal sealed class BudgetCreateCommandHandler(
        IBudgetRepository _repository,
        IPersonalCategoryRepository _personalCategoryRepository,
        IValidator<BudgetCreateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<BudgetCreateCommand, BudgetDto>
    {
        #region Public Methods

        public async Task<Result<BudgetDto>> Handle(BudgetCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<BudgetDto>(Error.Validation, validationResult.ToErrorList());
            }
            foreach (var item in request.BudgetPersonalCategories!)
            {
                var exists = await _personalCategoryRepository.ExistsAsync(Guid.Parse(item.PersonalCategoryId!));
                if (!exists)
                {
                    validationResult.Errors.Add(new ValidationFailure("Personal Category", "One or more personal category not found"));
                }
            }
            var existsByName = await _repository.ExistsByNameAsync(request.Name!);
            if (existsByName)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", "Budget with same name found"));
            }
            var amount = Decimal.Parse(request.Amount!);
            var startDate = DateTime.SpecifyKind(DateTime.Parse(request.StartDate!), DateTimeKind.Local).ToUniversalTime();
            var endDate = DateTime.SpecifyKind(DateTime.Parse(request.EndDate!), DateTimeKind.Local).ToUniversalTime();
            var entity = KopiBudget.Domain.Entities.Budget.Create(amount, request.Name!, startDate, endDate, request.UserId, request.UserId, DateTime.UtcNow);

            if (amount != request.BudgetPersonalCategories!.Sum(it => Decimal.Parse(it.Limit!)))
            {
                validationResult.Errors.Add(new ValidationFailure("Total amount", "Limits sum is not equal to budget amount"));
            }
            if (!validationResult.IsValid)
            {
                return Result.Failure<BudgetDto>(Error.Validation, validationResult.ToErrorList());
            }
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            foreach (var item in request.BudgetPersonalCategories!)
            {
                var limit = decimal.Parse(item.Limit!);
                var personalCategoryId = Guid.Parse(item.PersonalCategoryId!);
                entity.AddPersonalCategory(BudgetPersonalCategory.Create(personalCategoryId, entity.Id!.Value, limit, 0));
            }
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<BudgetDto>(entity));
        }

        #endregion Public Methods
    }
}