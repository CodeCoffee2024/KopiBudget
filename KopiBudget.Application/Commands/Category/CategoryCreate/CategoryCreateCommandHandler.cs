using AutoMapper;
using FluentValidation;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Category.CategoryCreate
{
    internal sealed class CategoryCreateCommandHandler(
        ICategoryRepository _repository,
        IValidator<CategoryCreateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<CategoryCreateCommand, CategoryDto>
    {
        #region Public Methods

        public async Task<Result<CategoryDto>> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<CategoryDto>(Error.Validation, validationResult.ToErrorList());
            }

            var entity = KopiBudget.Domain.Entities.Category.Create(request.Name, request.UserId, DateTime.UtcNow);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<CategoryDto>(entity));
        }

        #endregion Public Methods
    }
}