namespace KopiBudget.Application.Dtos
{
    public record AccountDropdownDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public bool IsDebt { get; set; }
        public CategoryDto Category { get; set; } = new CategoryDto();

        #endregion Properties
    }
}