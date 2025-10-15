using RelationshipApp.Core.Entities;

namespace RelationshipApp.Services.Interfaces;

public interface IAuthService
{
    Task<(User? user, string? token, string? refreshToken)> RegisterAsync(string email, string password, string displayName);
    Task<(User? user, string? token, string? refreshToken)> LoginAsync(string email, string password);
    Task<(User? user, string? token)> RefreshTokenAsync(string refreshToken);
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
}
