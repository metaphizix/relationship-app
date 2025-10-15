using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationshipApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "couples",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_couples", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "question_cards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    pack = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    question_text = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_cards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    display_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_rounds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    round_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_rounds", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_rounds_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anonymous_notes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    note_text = table.Column<string>(type: "text", nullable: false),
                    submitted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revealed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    submit_user_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anonymous_notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_anonymous_notes_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_anonymous_notes_users_submit_user_id",
                        column: x => x.submit_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "couple_members",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    joined_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_couple_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_couple_members_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_couple_members_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    target_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    progress = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goals", x => x.id);
                    table.ForeignKey(
                        name: "FK_goals_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goals_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "likes_dislikes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_revealed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes_dislikes", x => x.id);
                    table.ForeignKey(
                        name: "FK_likes_dislikes_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_dislikes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memory_board_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    media_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_private = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memory_board_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_memory_board_items_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_memory_board_items_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "moods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mood = table.Column<int>(type: "integer", nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moods", x => x.id);
                    table.ForeignKey(
                        name: "FK_moods_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_moods_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personality_tests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    test_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    answers = table.Column<string>(type: "jsonb", nullable: false),
                    score = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personality_tests", x => x.id);
                    table.ForeignKey(
                        name: "FK_personality_tests_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personality_tests_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "streaks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    couple_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_checked_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    current_streak = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streaks", x => x.id);
                    table.ForeignKey(
                        name: "FK_streaks_couples_couple_id",
                        column: x => x.couple_id,
                        principalTable: "couples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streaks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anonymous_notes_couple_id",
                table: "anonymous_notes",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_anonymous_notes_submit_user_id",
                table: "anonymous_notes",
                column: "submit_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_anonymous_notes_submitted_at",
                table: "anonymous_notes",
                column: "submitted_at");

            migrationBuilder.CreateIndex(
                name: "IX_couple_members_couple_id",
                table: "couple_members",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_couple_members_user_id",
                table: "couple_members",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_couples_code",
                table: "couples",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_couples_created_at",
                table: "couples",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_game_rounds_couple_id",
                table: "game_rounds",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_rounds_created_at",
                table: "game_rounds",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_goals_couple_id",
                table: "goals",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_goals_created_by_user_id",
                table: "goals",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_likes_dislikes_couple_id",
                table: "likes_dislikes",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_likes_dislikes_created_at",
                table: "likes_dislikes",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_likes_dislikes_user_id",
                table: "likes_dislikes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_memory_board_items_couple_id",
                table: "memory_board_items",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_memory_board_items_created_at",
                table: "memory_board_items",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_memory_board_items_user_id",
                table: "memory_board_items",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_moods_couple_id",
                table: "moods",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_moods_created_at",
                table: "moods",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_moods_user_id",
                table: "moods",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_personality_tests_couple_id",
                table: "personality_tests",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_personality_tests_created_at",
                table: "personality_tests",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_personality_tests_user_id",
                table: "personality_tests",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_cards_created_at",
                table: "question_cards",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_question_cards_pack",
                table: "question_cards",
                column: "pack");

            migrationBuilder.CreateIndex(
                name: "IX_streaks_couple_id",
                table: "streaks",
                column: "couple_id");

            migrationBuilder.CreateIndex(
                name: "IX_streaks_user_id",
                table: "streaks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_created_at",
                table: "users",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anonymous_notes");

            migrationBuilder.DropTable(
                name: "couple_members");

            migrationBuilder.DropTable(
                name: "game_rounds");

            migrationBuilder.DropTable(
                name: "goals");

            migrationBuilder.DropTable(
                name: "likes_dislikes");

            migrationBuilder.DropTable(
                name: "memory_board_items");

            migrationBuilder.DropTable(
                name: "moods");

            migrationBuilder.DropTable(
                name: "personality_tests");

            migrationBuilder.DropTable(
                name: "question_cards");

            migrationBuilder.DropTable(
                name: "streaks");

            migrationBuilder.DropTable(
                name: "couples");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
