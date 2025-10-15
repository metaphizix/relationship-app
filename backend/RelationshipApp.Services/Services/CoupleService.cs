using Microsoft.EntityFrameworkCore;
using RelationshipApp.Core.Entities;
using RelationshipApp.Infrastructure.Data;
using RelationshipApp.Services.Interfaces;
using System.Security.Cryptography;

namespace RelationshipApp.Services.Services;

public class CoupleService : ICoupleService
{
    private readonly AppDbContext _context;

    public CoupleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Couple?> CreateCoupleAsync(Guid userId, string? customCode = null)
    {
        // Check if user already has a couple
        var existingMembership = await _context.CoupleMembers
            .FirstOrDefaultAsync(cm => cm.UserId == userId);

        if (existingMembership != null)
        {
            return null; // User is already in a couple
        }

        // Generate or use custom code
        var code = customCode ?? GenerateCoupleCode();

        // Check if code is unique
        var existingCouple = await _context.Couples
            .FirstOrDefaultAsync(c => c.Code == code);

        if (existingCouple != null)
        {
            return null; // Code already exists
        }

        // Create new couple
        var couple = new Couple
        {
            Id = Guid.NewGuid(),
            Code = code,
            CreatedAt = DateTime.UtcNow
        };

        _context.Couples.Add(couple);

        // Add creator as first member
        var coupleMember = new CoupleMember
        {
            Id = Guid.NewGuid(),
            CoupleId = couple.Id,
            UserId = userId,
            Role = "partnerA",
            JoinedAt = DateTime.UtcNow
        };

        _context.CoupleMembers.Add(coupleMember);
        await _context.SaveChangesAsync();

        return couple;
    }

    public async Task<Couple?> JoinCoupleAsync(Guid userId, string code)
    {
        // Check if user already has a couple
        var existingMembership = await _context.CoupleMembers
            .FirstOrDefaultAsync(cm => cm.UserId == userId);

        if (existingMembership != null)
        {
            return null; // User is already in a couple
        }

        // Find couple by code
        var couple = await _context.Couples
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Code == code);

        if (couple == null)
        {
            return null; // Couple not found
        }

        // Check if couple already has 2 members
        if (couple.Members.Count >= 2)
        {
            return null; // Couple is full
        }

        // Add user as second member
        var coupleMember = new CoupleMember
        {
            Id = Guid.NewGuid(),
            CoupleId = couple.Id,
            UserId = userId,
            Role = "partnerB",
            JoinedAt = DateTime.UtcNow
        };

        _context.CoupleMembers.Add(coupleMember);
        await _context.SaveChangesAsync();

        return couple;
    }

    public async Task<Couple?> GetCoupleByIdAsync(Guid coupleId)
    {
        return await _context.Couples
            .Include(c => c.Members)
            .ThenInclude(cm => cm.User)
            .FirstOrDefaultAsync(c => c.Id == coupleId);
    }

    public async Task<Couple?> GetCoupleByUserIdAsync(Guid userId)
    {
        var coupleMember = await _context.CoupleMembers
            .Include(cm => cm.Couple)
            .ThenInclude(c => c.Members)
            .ThenInclude(cm => cm.User)
            .FirstOrDefaultAsync(cm => cm.UserId == userId);

        return coupleMember?.Couple;
    }

    public async Task<bool> IsUserInCoupleAsync(Guid userId, Guid coupleId)
    {
        return await _context.CoupleMembers
            .AnyAsync(cm => cm.UserId == userId && cm.CoupleId == coupleId);
    }

    public string GenerateCoupleCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var randomBytes = new byte[6];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var code = new char[6];
        for (int i = 0; i < 6; i++)
        {
            code[i] = chars[randomBytes[i] % chars.Length];
        }

        return new string(code);
    }
}
