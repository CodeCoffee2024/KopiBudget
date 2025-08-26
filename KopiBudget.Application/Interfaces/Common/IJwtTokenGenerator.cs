using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Interfaces.Common
{
    public interface IJwtTokenGenerator
    {
        #region Public Methods

        AuthDto GenerateToken(User user);

        Task<AuthDto> RefreshToken(string refreshToken);

        #endregion Public Methods
    }
}