namespace RelationshipApp.Core.Entities;

public class Couple
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<CoupleMember> Members { get; set; } = new List<CoupleMember>();
    public ICollection<LikeDislike> LikesDislikes { get; set; } = new List<LikeDislike>();
    public ICollection<PersonalityTest> PersonalityTests { get; set; } = new List<PersonalityTest>();
    public ICollection<Mood> Moods { get; set; } = new List<Mood>();
    public ICollection<MemoryBoardItem> MemoryBoardItems { get; set; } = new List<MemoryBoardItem>();
    public ICollection<GameRound> GameRounds { get; set; } = new List<GameRound>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<AnonymousNote> AnonymousNotes { get; set; } = new List<AnonymousNote>();
    public ICollection<Streak> Streaks { get; set; } = new List<Streak>();
}
