namespace KopiBudget.Domain.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        #region Public Constructors

        public InvalidEmailException(string email)
            : base($"Email '{email}' is invalid.") { }

        #endregion Public Constructors
    }
}