namespace KopiBudget.Application.Dtos
{
    public record BudgetDto : AuditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal SpentBudget { get; set; }
        public decimal RemainingBudget { get; set; }
        public string SpentBudgetPercentage { get; set; } = string.Empty;
        public string RemainingBudgetPercentage { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<BudgetPersonalCategoryDto> BudgetPersonalCategories { get; set; } = new List<BudgetPersonalCategoryDto>();
    }
}