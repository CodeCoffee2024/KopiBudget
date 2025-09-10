namespace KopiBudget.Application.Dtos
{
    public record TransactionDto : AuditDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public CategoryDto Category { get; set; } = new CategoryDto();
        public AccountDto Account { get; set; } = new AccountDto();
    }
}