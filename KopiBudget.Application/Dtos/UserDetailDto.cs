namespace KopiBudget.Application.Dtos
{
    public class UserDetailDto
    {
        #region Properties

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<UserRoleDto> Roles { get; set; } = new();
        public List<PermissionDto> Permissions { get; set; } = new();

        #endregion Properties
    }
}