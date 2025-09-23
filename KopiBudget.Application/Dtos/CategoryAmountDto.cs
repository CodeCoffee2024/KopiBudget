namespace KopiBudget.Application.Dtos
{
    public class CategoryAmountDto
    {
        #region Properties

        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        #endregion Properties
    }
}