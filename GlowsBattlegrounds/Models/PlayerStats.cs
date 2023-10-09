using System.ComponentModel.DataAnnotations;

namespace GlowsBattlegrounds.Models;

public class PlayerStats
{
    [Key]
    public string steamId { get; set; }
    public string gamertag { get; set; }
    public string weaponKillsBlob { get; set; }
    public string weaponDeathsBlob { get; set; }
    public string mapBlob { get; set; }
    public int totalGames { get; set; }
    public double averageKd { get; set; }
    public double averageKpm { get; set; }
    public double averageDpm { get; set; }
    public double mostKills { get; set; }
    public double totalKills { get; set; }
    public double totalDeaths { get; set; }
    public double mostDeaths { get; set; }
    public double highestKillStreak { get; set; }
    public double highestDeathStreak { get; set; }
    public double timesBetrayed { get; set; }
    public string firstSeen { get; set; }
    public string lastSeen { get; set; }
    public double totalTeamkills { get; set; }
}