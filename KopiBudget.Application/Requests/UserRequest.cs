using KopiBudget.Application.Commands.User;

namespace KopiBudget.Application.Requests
{
    public class UserRequest
    {
        #region Properties

        public string? Email { get; set; } = default!;
        public string? UserName { get; set; } = default!;
        public string? Password { get; set; } = default!;
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? MiddleName { get; set; } = default!;

        #endregion Properties

        #region Public Methods

        public UserRegisterCommand SetRegisterCommand() =>
            new(UserName!, Email!, Password!, FirstName!, LastName!, MiddleName!);

        #endregion Public Methods
    }
}