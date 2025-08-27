namespace KopiBudget.Domain.Abstractions
{
    public abstract class PagedQuery
    {
        #region Fields

        private const int MaxPageSize = 100;

        private int _pageSize = 10;

        #endregion Fields

        #region Properties

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Search { get; set; }
        public string? Status { get; set; }
        public string? OrderBy { get; set; }

        #endregion Properties
    }
}