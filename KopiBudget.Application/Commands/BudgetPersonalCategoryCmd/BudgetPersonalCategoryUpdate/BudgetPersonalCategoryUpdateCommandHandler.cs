using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.BudgetPersonalCategoryCmd.BudgetPersonalCategoryUpdate
{
    internal sealed class BudgetPersonalCategoryUpdateCommandHandler(
        IBudgetPersonalCategoryRepository _repository,
        IValidator<BudgetPersonalCategoryUpdateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<BudgetPersonalCategoryUpdateCommand, BudgetPersonalCategoryDto>
    {
        #region Public Methods

        public async Task<Result<BudgetPersonalCategoryDto>> Handle(BudgetPersonalCategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                return Result.Failure<BudgetPersonalCategoryDto>(Error.Validation, validation.ToErrorList());
            }

            if (!Guid.TryParse(request.BudgetId, out var budgetId) ||
                !Guid.TryParse(request.PersonalCategoryId, out var personalId))
            {
                return Result.Failure<BudgetPersonalCategoryDto>(Error.Notfound("Budget or Personal Category"));
            }

            if (!decimal.TryParse(request.Limit, out var limit))
            {
                validation.Errors.Add(new ValidationFailure("Limit", "Limit format is invalid"));
            }
            if (limit <= 0)
            {
                validation.Errors.Add(new ValidationFailure("Limit", "Limit must be greater than 0"));
            }

            var entity = await _repository.GetByBudgetIdAndPersonalCategoryIdAsync(budgetId, personalId);
            if (entity == null)
            {
                return Result.Failure<BudgetPersonalCategoryDto>(Error.Notfound("Budget or Personal Category"));
            }
            if (entity.RemainingLimit() < limit)
            {
                validation.Errors.Add(new ValidationFailure("Limit", "Limit exceeds budget amount"));
            }
            entity.UpdateLimit(limit);

            if (!validation.IsValid)
            {
                return Result.Failure<BudgetPersonalCategoryDto>(Error.Validation, validation.ToErrorList());
            }
            Console.WriteLine(entity.RemainingLimit());
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<BudgetPersonalCategoryDto>(entity));
        }

        #endregion Public Methods
    }
}