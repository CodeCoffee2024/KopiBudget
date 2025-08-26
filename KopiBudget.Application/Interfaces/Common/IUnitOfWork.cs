namespace KopiBudget.Application.Interfaces.Common
{
    public interface IUnitOfWork
    {
        #region Public Methods

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Public Methods
    }
}