using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.User.UserUpdateProfile
{
    internal sealed class UserUpdateProfileCommandHandler(
        IUserRepository _repository,
        IMapper _mappper,
        IFileService _fileService,
        IValidator<UserUpdateProfileCommand> _validator,
        IUnitOfWork _unitOfWork) : ICommandHandler<UserUpdateProfileCommand, UserUpdateProfileDto>
    {
        #region Public Methods

        public async Task<Result<UserUpdateProfileDto>> Handle(UserUpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<UserUpdateProfileDto>(Error.Validation, validation.ToErrorList());

            var user = await _repository.GetByIdAsync(request.Id);
            var idByUsername = await _repository.GetByUsernameAsync(request.UserName);

            if (await _repository.UsernameExists(request.UserName))
            {
                if (idByUsername!.Id != request.Id)
                {
                    validation.Errors.Add(new ValidationFailure("Username", "Username already exists."));
                }
            }
            user!.Update(request.FirstName, request.LastName, request.MiddleName, DateTime.UtcNow, request.Id);

            if (request.Img != null && request.Img.Length > 0)
            {
                string uniqueFileName = await _fileService.UploadImage(request.Img);
                user.UpdateImage(uniqueFileName);
            }
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mappper.Map<UserUpdateProfileDto>(user));
        }

        #endregion Public Methods
    }
}