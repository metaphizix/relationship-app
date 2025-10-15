namespace RelationshipApp.Core.Entities;

public class MemoryBoardItem
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? MediaUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPrivate { get; set; }

    // Navigation properties
    public Couple Couple { get; set; } = null!;
    public User User { get; set; } = null!;
}
