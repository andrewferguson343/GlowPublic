using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlowsBattlegrounds.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalStats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    totalKills = table.Column<int>(type: "int", nullable: false),
                    totalDeaths = table.Column<int>(type: "int", nullable: false),
                    mapDataBlob = table.Column<int>(type: "int", nullable: false),
                    highestKills = table.Column<int>(type: "int", nullable: false),
                    highestKillsSteamId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    highestKillstreak = table.Column<int>(type: "int", nullable: false),
                    killRecordBlob = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalStats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    steamId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    gamertag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weaponKillsBlob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weaponDeathsBlob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalGames = table.Column<int>(type: "int", nullable: false),
                    averageKd = table.Column<double>(type: "float", nullable: false),
                    averageKpm = table.Column<double>(type: "float", nullable: false),
                    averageDpm = table.Column<double>(type: "float", nullable: false),
                    mostKills = table.Column<double>(type: "float", nullable: false),
                    mostDeaths = table.Column<double>(type: "float", nullable: false),
                    highestKillStreak = table.Column<double>(type: "float", nullable: false),
                    highestDeathStreak = table.Column<double>(type: "float", nullable: false),
                    timesBetrayed = table.Column<double>(type: "float", nullable: false),
                    firstSeen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastSeen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => x.steamId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalStats");

            migrationBuilder.DropTable(
                name: "PlayerStats");
        }
    }
}
