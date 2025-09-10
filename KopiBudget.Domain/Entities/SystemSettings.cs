using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class SystemSettings : AuditableEntity
    {
        #region Private Constructors

        protected SystemSettings()
        { }

        #endregion Private Constructors

        #region Properties

        private SystemSettings(string currency)
        {
            Currency = currency;
        }

        public string Currency { get; private set; } = string.Empty;

        public static SystemSettings Create(string currency, Guid createdById)
        {
            var entity = new SystemSettings(currency);
            entity.SetCreated(createdById, DateTime.UtcNow);
            return entity;
        }

        public SystemSettings UpdateCurrency(string currency, Guid updatedById)
        {
            Currency = currency;
            SetUpdated(updatedById, DateTime.UtcNow);
            return this;
        }

        #endregion Properties
    }
}