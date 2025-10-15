namespace RelationshipApp.Core.Entities;

public class Streak
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public Guid UserId { get; set; }
    public DateTime LastCheckedDate { get; set; }
    public int CurrentStreak { get; set; }

    // Navigation properties
    public Couple Couple { get; set; } = null!;
    public User User { get; set; } = null!;
}
