using KopiBudget.Application.Commands.User.UserRegister;
using KopiBudget.Application.Commands.User.UserUpdateProfile;
using Microsoft.AspNetCore.Http;

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
        public IFormFile? Img { get; set; } = default!;

        #endregion Properties

        #region Public Methods

        public UserRegisterCommand SetRegisterCommand() =>
            new(UserName!, Email!, Password!, FirstName!, LastName!, MiddleName!);

        public UserUpdateProfileCommand SetUpdateUserProfileCommand(Guid Id, Guid UserId) =>
            new(LastName!, FirstName!, MiddleName!, UserName!, Password!, Img!, Id, UserId);

        #endregion Public Methods
    }
}