namespace KopiBudget.Domain.Abstractions
{
    public class PageResult<T>
    {
        #region Public Constructors

        public PageResult(IReadOnlyList<T> items, int totalCount, int pageNumber, int pageSize, string orderBy)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderBy;
        }

        #endregion Public Constructors

        #region Properties

        public IReadOnlyList<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        #endregion Properties
    }
}