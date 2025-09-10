using KopiBudget.Application.Dtos;

namespace KopiBudget.Application.Interfaces.Services
{
    public interface ISystemSettingsService
    {
        #region Public Methods

        IEnumerable<ModuleGroupDto> GetAllModuleGroups();

        IEnumerable<CurrencyDto> GetAllCurrencies();

        #endregion Public Methods
    }
}