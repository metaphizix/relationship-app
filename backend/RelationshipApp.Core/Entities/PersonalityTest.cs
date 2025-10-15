namespace RelationshipApp.Core.Entities;

public class PersonalityTest
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public Guid UserId { get; set; }
    public string TestType { get; set; } = string.Empty;
    public string Answers { get; set; } = "{}"; // JSON string
    public string Score { get; set; } = "{}"; // JSON string
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Couple Couple { get; set; } = null!;
    public User User { get; set; } = null!;
}
