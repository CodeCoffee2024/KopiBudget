using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetModuleGroupList
{
    public class GetModuleGroupListQueryHandler(
        ISystemSettingsService _moduleGroupService
    ) : IRequestHandler<GetModuleGroupListQuery, Result<IEnumerable<ModuleGroupDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<ModuleGroupDto>>> Handle(GetModuleGroupListQuery request, CancellationToken cancellationToken)
        {
            var result = _moduleGroupService.GetAllModuleGroups();

            return Result.Success(result);
        }

        #endregion Public Methods
    }
}