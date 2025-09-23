namespace KopiBudget.Application.Dtos
{
    public record DashboardBalanceExpenseDto : DashboardSummaryDto
    {
        public decimal PreviousMonth { get; set; }
    }
}