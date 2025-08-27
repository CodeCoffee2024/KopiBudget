using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Interfaces.Services
{
    public interface IModuleGroupService
    {
        #region Public Methods

        IEnumerable<ModuleGroupDto> GetAllModuleGroups();

        #endregion Public Methods
    }
}