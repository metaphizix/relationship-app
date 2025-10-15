namespace RelationshipApp.Core.Entities;

public class LikeDislike
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public Guid UserId { get; set; }
    public string Type { get; set; } = string.Empty; // "like" or "dislike"
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsRevealed { get; set; }

    // Navigation properties
    public Couple Couple { get; set; } = null!;
    public User User { get; set; } = null!;
}
