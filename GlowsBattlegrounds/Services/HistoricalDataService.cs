using System.Data.SqlTypes;
using GlowsBattlegrounds.CustomModels;
using GlowsBattlegrounds.Models;
using GlowsBattlegrounds.Pages;
using Newtonsoft.Json;

namespace GlowsBattlegrounds.Services;

public class HistoricalDataService : IHistoricalDataService
{
    
    
    private readonly ILogger<HistoricalDataService> _logger;
    private readonly StatsContext _statsContext;
    private int lastProcessedGame = 0;

    public HistoricalDataService(ILogger<HistoricalDataService> logger, StatsContext statsContext)
    {
        _logger = logger;
        _statsContext = statsContext;
    }
    public async Task<Boolean> SyncFromLastProcessedGame()
    {
        HttpClient httpClient = new HttpClient();

        _logger.LogInformation("Retrieving Historical Stats starting from match ID " + lastProcessedGame);
        HttpResponseMessage response = await httpClient.GetAsync("http://glows.gg:8013/api/get_map_scoreboard?map_id=30000");

        String responseString = await response.Content.ReadAsStringAsync();
        responseString = responseString.Replace("_", "");


        try
        {
            ResultDto historicalMapStats = JsonConvert.DeserializeObject<ResultDto>(responseString);

            PlayerStats playerStats = await ProcessPlayerStats(historicalMapStats);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error deserializing stats... Aborting");
            return false;
        }

        return true;
    }

    private async Task<PlayerStats> ProcessPlayerStats(ResultDto mapStats)
    {
        List<PlayerDto> playerRecords = mapStats.result.playerStats;
        for (int i = 0; i <1; i++)
        {
           
                PlayerDto playerToProcess = playerRecords[i];
                PlayerStats historicalStatsForPlayer = await _statsContext.FindAsync<PlayerStats>(playerToProcess.steamId64);
                if (historicalStatsForPlayer != null)
                {
                     ProcessNewPlayerRecord(playerToProcess, mapStats.result.mapName, mapStats.result.start );
                }
                else
                {
                    AddStatsToExistingPlayerRecord();
                }
            
           
        }

        return null;
    }

    private Boolean ProcessNewPlayerRecord(PlayerDto playerDto, String mapName, String matchStart)
    {
        PlayerStats playerStats = new PlayerStats();

        Dictionary<String, String> mapStats = new Dictionary<string, string>();
        mapStats.Add(mapName, "1");
        playerStats.steamId = playerDto.steamId64;
        playerStats.gamertag = playerDto.steamInfo.profile.personaName;
        playerStats.averageDpm = playerDto.deathsPerMinute;
        playerStats.averageKd = playerDto.killDeathRatio;
        playerStats.lastSeen = matchStart;
        playerStats.firstSeen = matchStart;
        playerStats.mostDeaths = playerDto.deaths;
        playerStats.totalDeaths = playerDto.deaths;
        playerStats.totalKills = playerDto.kills;
        playerStats.mostKills = playerDto.kills;
        playerStats.weaponKillsBlob = JsonConvert.ToString(playerDto.weapons);
        playerStats.weaponDeathsBlob = JsonConvert.ToString(playerDto.deathByWeapons);
        playerStats.mapBlob = JsonConvert.ToString(mapStats);
        playerStats.timesBetrayed = playerDto.deathsByTk;
        playerStats.highestKillStreak = playerDto.killsStreak;
        return true;

    }
    
    private Boolean AddStatsToExistingPlayerRecord()
    {
        PlayerStats playerStats = new PlayerStats();
        //
        // Dictionary<String, String> mapStats = new Dictionary<string, string>();
        // mapStats.Add(mapName, "1");
        // playerStats.steamId = playerDto.steamId64;
        // playerStats.gamertag = playerDto.steamInfo.profile.personaName;
        // playerStats.averageDpm = playerDto.deathsPerMinute;
        // playerStats.averageKd = playerDto.killDeathRatio;
        // playerStats.lastSeen = matchStart;
        // playerStats.firstSeen = matchStart;
        // playerStats.mostDeaths = playerDto.deaths;
        // playerStats.totalDeaths = playerDto.deaths;
        // playerStats.totalKills = playerDto.kills;
        // playerStats.mostKills = playerDto.kills;
        // playerStats.weaponKillsBlob = JsonConvert.ToString(playerDto.weapons);
        // playerStats.weaponDeathsBlob = JsonConvert.ToString(playerDto.deathByWeapons);
        // playerStats.mapBlob = JsonConvert.ToString(mapStats);
        return true;

    }
}