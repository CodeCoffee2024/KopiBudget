namespace KopiBudget.Application.Dtos
{
    public record CategoryDropdownDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsExpense { get; set; }

        #endregion Properties
    }
}