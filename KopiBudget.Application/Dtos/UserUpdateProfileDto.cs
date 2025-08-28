namespace KopiBudget.Application.Dtos
{
    public record UserUpdateProfileDto : AuditDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Status { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string MiddleName { get; private set; } = string.Empty;
        public string Img { get; private set; } = string.Empty;

        #endregion Properties
    }
}