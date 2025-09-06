namespace KopiBudget.Api.Middleware.Authorization
{
    using KopiBudget.Application.Interfaces.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Security.Claims;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        #region Fields

        private readonly string _module;
        private readonly string _permission;

        #endregion Fields

        #region Public Constructors

        public PermissionAuthorizeAttribute(string module, string permission)
        {
            _module = module;
            _permission = permission;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var permissionService = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();

            // Get UserId from claims (or however your auth is set)
            var userIdClaim = context.HttpContext.User.FindFirst("sub") ?? context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            var hasAccess = await permissionService.HasPermissionAsync(userId, _module, _permission);
            if (!hasAccess)
            {
                context.Result = new ForbidResult();
            }
        }

        #endregion Public Methods
    }
}