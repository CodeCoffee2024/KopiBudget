using FluentValidation;
using KopiBudget.Application.Commands.User.UserUpdateProfile;

namespace KopiBudget.Application.Validators.User
{
    public class UserUpdateProfileValidator : AbstractValidator<UserUpdateProfileCommand>
    {
        #region Public Constructors

        public UserUpdateProfileValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(100);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password length must be atleast 6 characters");
        }

        #endregion Public Constructors
    }
}