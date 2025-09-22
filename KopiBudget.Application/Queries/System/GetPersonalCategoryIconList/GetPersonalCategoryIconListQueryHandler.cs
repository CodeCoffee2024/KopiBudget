using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetPersonalCategoryIconList
{
    public class GetPersonalCategoryIconListQueryHandler(
        ISystemSettingsService _service
    ) : IRequestHandler<GetPersonalCategoryIconListQuery, Result<PersonalCategoryFieldDto>>
    {
        #region Public Methods

        public async Task<Result<PersonalCategoryFieldDto>> Handle(GetPersonalCategoryIconListQuery request, CancellationToken cancellationToken)
        {
            var result = _service.GetAllPersonalCategoryFields();

            return Result.Success(result);
        }

        #endregion Public Methods
    }
}