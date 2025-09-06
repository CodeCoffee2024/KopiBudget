namespace KopiBudget.Application.Interfaces.Services
{
    public interface IPermissionService
    {
        #region Public Methods

        Task<bool> HasPermissionAsync(Guid userId, string moduleName, string permissionName);

        #endregion Public Methods
    }
}