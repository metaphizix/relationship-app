using RelationshipApp.Core.Entities;

namespace RelationshipApp.Services.Interfaces;

public interface ICoupleService
{
    Task<Couple?> CreateCoupleAsync(Guid userId, string? customCode = null);
    Task<Couple?> JoinCoupleAsync(Guid userId, string code);
    Task<Couple?> GetCoupleByIdAsync(Guid coupleId);
    Task<Couple?> GetCoupleByUserIdAsync(Guid userId);
    Task<bool> IsUserInCoupleAsync(Guid userId, Guid coupleId);
    string GenerateCoupleCode();
}
