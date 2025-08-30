namespace KopiBudget.Api.Middleware.Authorization
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;
    using System.Security.Claims;

    public class AuthorizePermissionAttribute : Attribute, IAuthorizationFilter
    {
        #region Fields

        private readonly string[] _requiredPermissions;

        #endregion Fields

        #region Public Constructors

        public AuthorizePermissionAttribute(params string[] requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }

        #endregion Public Constructors

        #region Public Methods

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Retrieve user's permissions from claims
            var userPermissions = user.Claims
                .Where(c => c.Type == "Permissions")
                .SelectMany(c => c.Value.Split(",")) // ✅ Flattens into a single List<string>
                .Select(p => p.Trim()) // ✅ Removes extra spaces (if any)
                .Distinct() // ✅ Optional: Removes duplicates
                .ToList();

            bool hasAllPermissions = _requiredPermissions.All(p => userPermissions.Contains(p));
            if (!hasAllPermissions)
            {
                context.Result = new ForbidResult();
            }
        }

        public bool HasPermission(ClaimsPrincipal user, params string[] requiredPermissions)
        {
            if (user?.Identity is null || !user.Identity.IsAuthenticated)
                return false;

            var userPermissions = user.Claims
                .Where(c => c.Type == "Permissions")
                .SelectMany(c => c.Value.Split(","))
                .Select(p => p.Trim())
                .Distinct()
                .ToList();

            return requiredPermissions.All(p => userPermissions.Contains(p));
        }

        #endregion Public Methods
    }
}