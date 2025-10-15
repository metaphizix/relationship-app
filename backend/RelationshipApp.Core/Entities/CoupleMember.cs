namespace RelationshipApp.Core.Entities;

public class CoupleMember
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = string.Empty; // "partnerA" or "partnerB"
    public DateTime JoinedAt { get; set; }

    // Navigation properties
    public Couple Couple { get; set; } = null!;
    public User User { get; set; } = null!;
}
