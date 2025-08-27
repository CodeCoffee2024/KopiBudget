namespace KopiBudget.Application.Dtos
{
    public abstract record AuditDto
    {
        #region Properties

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public object? CreatedByUser { get; set; }
        public object? UpdatedByUser { get; set; }

        #endregion Properties
    }
}