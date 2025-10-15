namespace RelationshipApp.Core.Entities;

public class QuestionCard
{
    public Guid Id { get; set; }
    public string Pack { get; set; } = string.Empty;
    public string QuestionText { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
