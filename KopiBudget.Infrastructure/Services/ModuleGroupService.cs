using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Common.Constants;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Infrastructure.Services
{
    public class ModuleGroupService(IModuleRepository moduleRepository) : IModuleGroupService
    {
        #region Public Methods

        public IEnumerable<ModuleGroupDto> GetAllModuleGroups()
        {
            return ModuleGroups.GROUPS.Select(group => new ModuleGroupDto
            {
                Key = group.Key,
                Modules = moduleRepository.GetByModuleNames(group.Value)
                .Result.Select(m => new ModuleDto
                {
                    Id = m.Id!.Value,
                    Name = m.Name,
                    Link = m.Link
                }).ToList()
            });
        }

        #endregion Public Methods
    }
}