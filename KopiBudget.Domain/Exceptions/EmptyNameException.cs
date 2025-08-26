namespace KopiBudget.Domain.Exceptions
{
    public class EmptyNameException : DomainException
    {
        #region Public Constructors

        public EmptyNameException()
            : base("Name is required and cannot be empty.") { }

        #endregion Public Constructors
    }
}