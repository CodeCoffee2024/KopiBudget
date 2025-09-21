namespace KopiBudget.Application.Dtos
{
    public record BudgetPersonalCategoryDto
    {
        public string? Limit { get; set; }
        public string? PersonalCategoryId { get; set; }
        public decimal SpentBudget { get; set; }
        public decimal RemainingBudget { get; set; }
        public decimal RemainingLimit { get; set; }
        public string SpentBudgetPercentage { get; set; } = string.Empty;
        public string RemainingBudgetPercentage { get; set; } = string.Empty;
        public PersonalCategoryFragment? PersonalCategory { get; set; }
    }
}