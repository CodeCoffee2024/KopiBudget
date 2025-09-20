namespace KopiBudget.Application.Dtos
{
    public record BudgetDropdownDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        #endregion Properties
    }
}