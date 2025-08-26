namespace KopiBudget.Domain.Exceptions
{
    public class InvalidDateRangeException : DomainException
    {
        #region Public Constructors

        public InvalidDateRangeException(DateTime startDate, DateTime endDate)
            : base($"EndDate '{endDate}' must be after StartDate '{startDate}'.") { }

        #endregion Public Constructors
    }
}