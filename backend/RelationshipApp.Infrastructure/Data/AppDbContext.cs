using Microsoft.EntityFrameworkCore;
using RelationshipApp.Core.Entities;

namespace RelationshipApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Couple> Couples { get; set; }
    public DbSet<CoupleMember> CoupleMembers { get; set; }
    public DbSet<LikeDislike> LikesDislikes { get; set; }
    public DbSet<PersonalityTest> PersonalityTests { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<MemoryBoardItem> MemoryBoardItems { get; set; }
    public DbSet<QuestionCard> QuestionCards { get; set; }
    public DbSet<GameRound> GameRounds { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<AnonymousNote> AnonymousNotes { get; set; }
    public DbSet<Streak> Streaks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash").HasMaxLength(255).IsRequired();
            entity.Property(e => e.DisplayName).HasColumnName("display_name").HasMaxLength(255).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.CreatedAt);
        });

        // Couple configuration
        modelBuilder.Entity<Couple>(entity =>
        {
            entity.ToTable("couples");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.CreatedAt);
        });

        // CoupleMember configuration
        modelBuilder.Entity<CoupleMember>(entity =>
        {
            entity.ToTable("couple_members");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(50).IsRequired();
            entity.Property(e => e.JoinedAt).HasColumnName("joined_at");
            
            entity.HasOne(e => e.Couple)
                .WithMany(c => c.Members)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.CoupleMembers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.UserId);
        });

        // LikeDislike configuration
        modelBuilder.Entity<LikeDislike>(entity =>
        {
            entity.ToTable("likes_dislikes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Type).HasColumnName("type").HasMaxLength(20).IsRequired();
            entity.Property(e => e.Text).HasColumnName("text").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsRevealed).HasColumnName("is_revealed");

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.LikesDislikes)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.LikesDislikes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CreatedAt);
        });

        // PersonalityTest configuration
        modelBuilder.Entity<PersonalityTest>(entity =>
        {
            entity.ToTable("personality_tests");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TestType).HasColumnName("test_type").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Answers).HasColumnName("answers").HasColumnType("jsonb");
            entity.Property(e => e.Score).HasColumnName("score").HasColumnType("jsonb");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.PersonalityTests)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.PersonalityTests)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CreatedAt);
        });

        // Mood configuration
        modelBuilder.Entity<Mood>(entity =>
        {
            entity.ToTable("moods");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.MoodValue).HasColumnName("mood");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne(e => e.User)
                .WithMany(u => u.Moods)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.Moods)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.CreatedAt);
        });

        // MemoryBoardItem configuration
        modelBuilder.Entity<MemoryBoardItem>(entity =>
        {
            entity.ToTable("memory_board_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Content).HasColumnName("content").IsRequired();
            entity.Property(e => e.MediaUrl).HasColumnName("media_url").HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsPrivate).HasColumnName("is_private");

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.MemoryBoardItems)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.MemoryBoardItems)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CreatedAt);
        });

        // QuestionCard configuration
        modelBuilder.Entity<QuestionCard>(entity =>
        {
            entity.ToTable("question_cards");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pack).HasColumnName("pack").HasMaxLength(100).IsRequired();
            entity.Property(e => e.QuestionText).HasColumnName("question_text").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            
            entity.HasIndex(e => e.Pack);
            entity.HasIndex(e => e.CreatedAt);
        });

        // GameRound configuration
        modelBuilder.Entity<GameRound>(entity =>
        {
            entity.ToTable("game_rounds");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.RoundType).HasColumnName("round_type").HasMaxLength(100).IsRequired();
            entity.Property(e => e.State).HasColumnName("state").HasColumnType("jsonb");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.GameRounds)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.CreatedAt);
        });

        // Goal configuration
        modelBuilder.Entity<Goal>(entity =>
        {
            entity.ToTable("goals");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TargetDate).HasColumnName("target_date");
            entity.Property(e => e.Progress).HasColumnName("progress").HasColumnType("jsonb");

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.Goals)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.CreatedByUserId);
        });

        // AnonymousNote configuration
        modelBuilder.Entity<AnonymousNote>(entity =>
        {
            entity.ToTable("anonymous_notes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.NoteText).HasColumnName("note_text").IsRequired();
            entity.Property(e => e.SubmittedAt).HasColumnName("submitted_at");
            entity.Property(e => e.RevealedAt).HasColumnName("revealed_at");
            entity.Property(e => e.SubmitUserId).HasColumnName("submit_user_id");

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.AnonymousNotes)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.SubmitUser)
                .WithMany()
                .HasForeignKey(e => e.SubmitUserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.SubmittedAt);
        });

        // Streak configuration
        modelBuilder.Entity<Streak>(entity =>
        {
            entity.ToTable("streaks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoupleId).HasColumnName("couple_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.LastCheckedDate).HasColumnName("last_checked_date");
            entity.Property(e => e.CurrentStreak).HasColumnName("current_streak");

            entity.HasOne(e => e.Couple)
                .WithMany(c => c.Streaks)
                .HasForeignKey(e => e.CoupleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Streaks)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.CoupleId);
            entity.HasIndex(e => e.UserId);
        });
    }
}
