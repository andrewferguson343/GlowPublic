namespace GlowsBattlegrounds.CustomModels;

public class PlayerDto
{
    public int id { get; set; }
    public int playerId { get; set; }
    public string creationTime { get; set; }
    public string steamId64 { get; set; }
    public SteamInfoDto steamInfo { get; set; }
    public int kills { get; set; }
    public int killsStreak { get; set; }
    public int deaths { get; set; }
    public int deathsWithoutKillStreak { get; set; }
    public int teamkills { get; set; }
    public int teamkillsStreak { get; set; }
    public int deathsByTk { get; set; }
    public int deathsByTkSstreak { get; set; }
    public int timeSeconds { get; set; }
    public int killsPerMinute { get; set; }
    public int deathsPerMinute { get; set; }
    public int killDeathRatio { get; set; }
    public int longestLifeSeconds { get; set; }
    public int combat { get; set; }
    public int offense { get; set; }
    public int defense { get; set; }
    public int support { get; set; }
    public Dictionary<string, string> weapons { get; set; }
    public Dictionary<string, string> deathByWeapons { get; set; }
    
}