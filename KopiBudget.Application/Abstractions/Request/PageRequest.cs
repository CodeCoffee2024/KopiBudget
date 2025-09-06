namespace KopiBudget.Application.Abstractions.Request
{
    public abstract class PageRequest
    {
        #region Properties

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; } = String.Empty;
        public string? Search { get; set; } = String.Empty;

        #endregion Properties
    }
}