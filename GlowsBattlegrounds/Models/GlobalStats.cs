namespace GlowsBattlegrounds.Models;

public class GlobalStats
{
    public int totalKills { get; set; }
    public int totalDeaths { get; set; }
    public int mapDataBlob { get; set; }
    public int highestKills { get; set; }
    public string highestKillsSteamId { get; set; }
    public int highestKillstreak { get; set; }
    public string killRecordBlob { get; set; }

    public int id { get; set; }
    }