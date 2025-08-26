namespace KopiBudget.Domain.Exceptions
{
    public class NegativeAmountException : DomainException
    {
        #region Public Constructors

        public NegativeAmountException(decimal amount)
            : base($"Amount '{amount}' is invalid. Negative amount is prohibited.") { }

        #endregion Public Constructors
    }
}