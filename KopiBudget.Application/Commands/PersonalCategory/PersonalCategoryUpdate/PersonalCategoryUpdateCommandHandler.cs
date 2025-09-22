using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryUpdate
{
    internal sealed class PersonalCategoryUpdateCommandHandler(
        IPersonalCategoryRepository _personalCategoryRepository,
        IValidator<PersonalCategoryUpdateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<PersonalCategoryUpdateCommand, PersonalCategoryDto>
    {
        #region Public Methods

        public async Task<Result<PersonalCategoryDto>> Handle(PersonalCategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<PersonalCategoryDto>(Error.Validation, validationResult.ToErrorList());
            }

            if (!Guid.TryParse(request.Id, out var id))
            {
                return Result.Failure<PersonalCategoryDto>(Error.Notfound("Personal Category"));
            }
            var entity = await _personalCategoryRepository.GetByIdAsync(id);
            var existsByName = await _personalCategoryRepository.GetByNameAsync(request.Name!);
            if (existsByName != null && existsByName.Id != id)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", "Personal category already exists"));
            }
            if (!validationResult.IsValid)
            {
                return Result.Failure<PersonalCategoryDto>(Error.Validation, validationResult.ToErrorList());
            }
            if (entity == null)
                return Result.Failure<PersonalCategoryDto>(Error.Notfound("Personal Category"));
            entity!.Update(request.Name!, request.Icon!, request.Color!, request.UserId, DateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<PersonalCategoryDto>(entity));
        }

        #endregion Public Methods
    }
}