using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DiscordBot.Database.RandomGame.Mirgrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GuildId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<string>(type: "TEXT", nullable: true),
                    DaysToRun = table.Column<int>(type: "INTEGER", nullable: false),
                    MinutesBetweenRequest = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "GameScores",
                columns: table => new
                {
                    GameScoreId = table.Column<Guid>(type: "TEXT", nullable: false),
                    userGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    NextScoreDate = table.Column<long>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    GameId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameScores", x => x.GameScoreId);
                    table.ForeignKey(
                        name: "FK_GameScores_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameScores_GameId",
                table: "GameScores",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameScores");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}