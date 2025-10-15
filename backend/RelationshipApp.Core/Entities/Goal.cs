namespace RelationshipApp.Core.Entities;

public class Goal
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime TargetDate { get; set; }
    public string Progress { get; set; } = "{}"; // JSON string

    // Navigation properties
    public Couple Couple { get; set; } = null!;
    public User CreatedByUser { get; set; } = null!;
}
