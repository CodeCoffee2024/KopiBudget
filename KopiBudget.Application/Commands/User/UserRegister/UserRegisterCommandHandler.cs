using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Commands.User.UserRegister
{
    internal sealed class UserRegisterCommandHandler(
        IUserRepository _repository,
        IPasswordHasherService _passwordHasherService,
        IMapper _mappper,
        IValidator<UserRegisterCommand> _validator,
        IUnitOfWork _unitOfWork
    ) : IRequestHandler<UserRegisterCommand, Result>
    {
        #region Public Methods

        public async Task<Result> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (await _repository.EmailExists(request.Email))
            {
                validationResult.Errors.Add(new ValidationFailure("Email", "Email already exists."));
            }
            if (await _repository.UsernameExists(request.UserName))
            {
                validationResult.Errors.Add(new ValidationFailure("UserName", "Username already exists."));
            }

            if (!validationResult.IsValid)
            {
                return Result.Failure<List<UserRegisterDto>>(Error.Validation, validationResult.ToErrorList());
            }
            var entity = Domain.Entities.User.Register(
                request.UserName,
                request.Email,
                _passwordHasherService.HashPassword(request.Password),
                request.FirstName,
                request.LastName,
                request.MiddleName
            );
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mappper.Map<UserRegisterDto>(entity));
        }

        #endregion Public Methods
    }
}