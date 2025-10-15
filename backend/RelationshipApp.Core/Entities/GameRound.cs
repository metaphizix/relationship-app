namespace RelationshipApp.Core.Entities;

public class GameRound
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public string RoundType { get; set; } = string.Empty;
    public string State { get; set; } = "{}"; // JSON string
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Couple Couple { get; set; } = null!;
}
