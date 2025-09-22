using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryCreate
{
    internal sealed class PersonalCategoryCreateCommandHandler(
        IPersonalCategoryRepository _repository,
        IValidator<PersonalCategoryCreateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<PersonalCategoryCreateCommand, PersonalCategoryDto>
    {
        #region Public Methods

        public async Task<Result<PersonalCategoryDto>> Handle(PersonalCategoryCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<PersonalCategoryDto>(Error.Validation, validationResult.ToErrorList());
            }
            var existsByName = await _repository.GetByNameAsync(request.Name!);
            if (existsByName != null)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", "Personal category already exists"));
            }
            if (!validationResult.IsValid)
            {
                return Result.Failure<PersonalCategoryDto>(Error.Validation, validationResult.ToErrorList());
            }
            var entity = KopiBudget.Domain.Entities.PersonalCategory.Create(request.Name, request.Icon, request.Color, request.UserId, DateTime.UtcNow);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<PersonalCategoryDto>(entity));
        }

        #endregion Public Methods
    }
}