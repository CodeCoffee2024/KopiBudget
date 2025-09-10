namespace KopiBudget.Application.Dtos
{
    public record CurrencyDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}