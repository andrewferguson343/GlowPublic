using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlowsBattlegrounds.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreStatsToPlayerStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mapBlob",
                table: "PlayerStats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "totalDeaths",
                table: "PlayerStats",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "totalKills",
                table: "PlayerStats",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "totalTeamkills",
                table: "PlayerStats",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mapBlob",
                table: "PlayerStats");

            migrationBuilder.DropColumn(
                name: "totalDeaths",
                table: "PlayerStats");

            migrationBuilder.DropColumn(
                name: "totalKills",
                table: "PlayerStats");

            migrationBuilder.DropColumn(
                name: "totalTeamkills",
                table: "PlayerStats");
        }
    }
}
