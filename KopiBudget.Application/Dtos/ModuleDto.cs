namespace KopiBudget.Application.Dtos
{
    public record ModuleDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        #endregion Properties
    }
}