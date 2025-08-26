using KopiBudget.Domain.Entities;

namespace KopiBudget.Domain.Abstractions
{
    public abstract class AuditableEntity : BaseEntity
    {
        #region Properties

        public DateTime? CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public virtual User? CreatedBy { get; init; }
        public virtual User? UpdatedBy { get; init; }
        public Guid? CreatedById { get; private set; }
        public Guid? UpdatedById { get; private set; }

        public object? CreatedByUser()
        {
            return CreatedBy != null
                ? new
                {
                    firstName = CreatedBy.FirstName,
                    lastName = CreatedBy.LastName
                }
                : null;
        }

        public object? UpdatedByUser()
        {
            return UpdatedBy != null
                ? new
                {
                    firstName = UpdatedBy.FirstName,
                    lastName = UpdatedBy.LastName
                }
                : null;
        }

        #endregion Properties

        #region Protected Methods

        protected void SetUpdated(Guid updatedById, DateTime updatedOn)
        {
            UpdatedById = updatedById;
            UpdatedOn = updatedOn;
        }

        protected void SetCreated(Guid createdById, DateTime createdOn)
        {
            CreatedById = createdById;
            CreatedOn = createdOn;
            UpdatedById = createdById;
            UpdatedOn = createdOn;
        }

        #endregion Protected Methods
    }
}