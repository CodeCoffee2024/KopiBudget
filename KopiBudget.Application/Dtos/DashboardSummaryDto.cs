namespace KopiBudget.Application.Dtos
{
    public record DashboardSummaryDto
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}