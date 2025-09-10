using KopiBudget.Domain.Entities;

namespace KopiBudget.Domain.Interfaces
{
    public interface ISystemSettingsRepository
    {
        #region Public Methods

        void Update(SystemSettings entity);

        Task<SystemSettings?> GetSettingsAsync();

        #endregion Public Methods
    }
}