namespace KopiBudget.Application.Dtos
{
    public record PermissionDto
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;

        #endregion Properties
    }
}