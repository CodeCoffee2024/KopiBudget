using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Infrastructure.Services
{
    public class PermissionService(IUserRepository _userRepository) : IPermissionService
    {
        #region Public Methods

        public async Task<bool> HasPermissionAsync(Guid userId, string moduleName, string permissionName)
        {
            return await _userRepository.HasPermission(userId, moduleName, permissionName);
        }

        #endregion Public Methods
    }
}