namespace RelationshipApp.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<CoupleMember> CoupleMembers { get; set; } = new List<CoupleMember>();
    public ICollection<LikeDislike> LikesDislikes { get; set; } = new List<LikeDislike>();
    public ICollection<PersonalityTest> PersonalityTests { get; set; } = new List<PersonalityTest>();
    public ICollection<Mood> Moods { get; set; } = new List<Mood>();
    public ICollection<MemoryBoardItem> MemoryBoardItems { get; set; } = new List<MemoryBoardItem>();
    public ICollection<Streak> Streaks { get; set; } = new List<Streak>();
}
