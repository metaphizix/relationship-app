namespace RelationshipApp.Core.Entities;

public class AnonymousNote
{
    public Guid Id { get; set; }
    public Guid CoupleId { get; set; }
    public string NoteText { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public DateTime? RevealedAt { get; set; }
    public Guid? SubmitUserId { get; set; }

    // Navigation properties
    public Couple Couple { get; set; } = null!;
    public User? SubmitUser { get; set; }
}
