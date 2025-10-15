namespace RelationshipApp.Core.Entities;

public class Mood
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CoupleId { get; set; }
    public int MoodValue { get; set; } // 1-5 scale
    public string Note { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Couple Couple { get; set; } = null!;
}
