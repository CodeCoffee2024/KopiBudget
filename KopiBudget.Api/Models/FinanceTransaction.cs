using System.ComponentModel.DataAnnotations;

namespace KopiBudget.Api.Models
{
    public class FinanceTransaction
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        public string? Notes { get; set; }

        #endregion Properties
    }
}