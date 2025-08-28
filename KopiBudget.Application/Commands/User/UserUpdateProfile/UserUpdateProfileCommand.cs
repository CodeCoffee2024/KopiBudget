using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace KopiBudget.Application.Commands.User.UserUpdateProfile
{
    public sealed record UserUpdateProfileCommand(
        string LastName,
        string FirstName,
        string MiddleName,
        string UserName,
        string Password,
        IFormFile Img,
        Guid Id,
        Guid UpdatedById) : ICommand<UserUpdateProfileDto>;
}