namespace KopiBudget.Application.Dtos
{
    public record PersonalCategoryDto : AuditDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        #endregion Properties
    }
    public record PersonalCategoryFragment
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        #endregion Properties
    }
}