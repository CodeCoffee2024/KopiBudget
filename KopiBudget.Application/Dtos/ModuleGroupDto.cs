namespace KopiBudget.Application.Dtos
{
    public record ModuleGroupDto
    {
        #region Properties

        public string Key { get; set; } = string.Empty;
        public List<ModuleDto> Modules { get; set; } = new();

        #endregion Properties
    }
}