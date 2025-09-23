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

namespace KopiBudget.Application.Commands.Budget.BudgetUpdate
{
    internal sealed class BudgetUpdateCommandHandler(
        IBudgetRepository _repository,
        IPersonalCategoryRepository _personalCategoryRepository,
        IValidator<BudgetUpdateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<BudgetUpdateCommand, BudgetDto>
    {
        #region Public Methods

        public async Task<Result<BudgetDto>> Handle(BudgetUpdateCommand request, CancellationToken cancellationToken)
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
            var id = Guid.Parse(request.Id!);
            var amount = Decimal.Parse(request.Amount!);
            var startDate = DateTime.SpecifyKind(DateTime.Parse(request.StartDate!), DateTimeKind.Local).ToUniversalTime();
            var endDate = DateTime.SpecifyKind(DateTime.Parse(request.EndDate!), DateTimeKind.Local).ToUniversalTime();
            var entity = await _repository.GetByIdAsync(id);
            entity!.Update(amount, request.Name!, startDate, endDate, request.UserId, DateTime.UtcNow);
            var existsByName = await _repository.GetByNameAsync(request.Name!);
            if (existsByName != null && existsByName.Id != id)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", "Budget with same name belongs to another entity"));
            }
            if (!validationResult.IsValid)
            {
                return Result.Failure<BudgetDto>(Error.Validation, validationResult.ToErrorList());
            }
            var existingIds = entity.BudgetPersonalCategories
            .Select(it => it.PersonalCategoryId)
            .ToHashSet(); // faster lookup

            var newBudgetPersonalCategories = request.BudgetPersonalCategories
                .Where(it => !existingIds.Contains(Guid.Parse(it.PersonalCategoryId!)))
                .ToList();

            var existingBudgetPersonalCategories = request.BudgetPersonalCategories
                .Where(it => existingIds.Contains(Guid.Parse(it.PersonalCategoryId!)))
                .ToList();

            foreach (var item in newBudgetPersonalCategories!)
            {
                var limit = decimal.Parse(item.Limit!);
                var personalCategoryId = Guid.Parse(item.PersonalCategoryId!);
                entity.AddPersonalCategory(BudgetPersonalCategory.Create(personalCategoryId, entity.Id!.Value, limit, 0));
            }
            foreach (var item in existingBudgetPersonalCategories!)
            {
                var existing = entity.BudgetPersonalCategories.Where(it => it.PersonalCategoryId == Guid.Parse(item.PersonalCategoryId!)).FirstOrDefault();
                var limit = decimal.Parse(item.Limit!);
                existing!.UpdateLimit(limit);
            }
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<BudgetDto>(entity));
        }

        #endregion Public Methods
    }
}