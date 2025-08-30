using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Common.Entities;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace KopiBudget.Infrastructure.Services
{
    public class JwtTokenGenerator(JwtSettings _settings, IUserRepository _userRepository) : IJwtTokenGenerator
    {
        #region Public Methods

        public AuthDto GenerateToken(User user)
        {
            AuthDto response = new AuthDto();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(user.UserRoles.Select(ur =>
                new Claim(ClaimTypes.Role, ur.Role!.Name)));

            response.ExpiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);
            response.RefreshTokenExpiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes * 3);
            response.Email = user.Email;
            response.Token = GenerateJwtToken(claims, response.ExpiresAt);
            response.RefreshToken = GenerateJwtToken(claims, response.RefreshTokenExpiresAt);
            return response;
        }

        public async Task<AuthDto> RefreshToken(string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(refreshToken);
            if (principal == null)
            {
                throw new SecurityException("Invalid refresh token.");
            }

            var identity = principal.Identity as ClaimsIdentity;
            var userId = identity!.Claims.FirstOrDefault(it => it.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityException("Invalid refresh token: no user identifier.");
            }

            // Extract expiration from JWT claims
            var expClaim = identity.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
            if (expClaim != null && long.TryParse(expClaim, out var expUnix))
            {
                var expDate = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                if (expDate < DateTime.UtcNow)
                {
                    throw new SecurityException("Refresh token has expired.");
                }
            }

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
            if (user == null)
            {
                throw new SecurityException("User not found for this refresh token.");
            }

            return GenerateToken(user);
        }

        private string GenerateJwtToken(List<Claim> claims, DateTime expires)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key))
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                var jwtToken = securityToken as JwtSecurityToken;

                if (jwtToken == null)
                {
                    Console.WriteLine("Invalid JWT token.");
                    return null;
                }

                Console.WriteLine($"Principal Created: {principal.Identity?.Name}");
                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }

        #endregion Public Methods
    }
}