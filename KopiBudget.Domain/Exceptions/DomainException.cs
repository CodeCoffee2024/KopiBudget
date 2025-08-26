namespace KopiBudget.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        #region Protected Constructors

        protected DomainException(string message) : base(message)
        {
        }

        #endregion Protected Constructors
    }
}