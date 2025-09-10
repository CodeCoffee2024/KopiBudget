using FluentValidation;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.System.SystemUpdateCurrency
{
    internal sealed class SystemUpdateCurrencyCommandHandler(
        ISystemSettingsRepository _repository,
        IValidator<SystemUpdateCurrencyCommand> _validator,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<SystemUpdateCurrencyCommand>
    {
        #region Public Methods

        public async Task<Result> Handle(SystemUpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<CurrencyDto>(Error.Validation, validationResult.ToErrorList());
            }

            var entity = await _repository.GetSettingsAsync();
            entity!.UpdateCurrency(request.Currency, request.UserId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        #endregion Public Methods
    }
}