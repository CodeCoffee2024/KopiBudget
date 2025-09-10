﻿namespace KopiBudget.Application.Dtos
{
    public record AccountDto : AuditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public bool IsDebt { get; set; }
        public CategoryDto Category { get; set; } = new CategoryDto();
    }
}