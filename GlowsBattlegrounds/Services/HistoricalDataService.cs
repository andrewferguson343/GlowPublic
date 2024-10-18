using System.Data.SqlTypes;
using System.Runtime.InteropServices.JavaScript;
using GlowsBattlegrounds.CustomModels;
using GlowsBattlegrounds.Models;
using GlowsBattlegrounds.Pages;
using Newtonsoft.Json;

namespace GlowsBattlegrounds.Services;

public class HistoricalDataService : IHistoricalDataService
{
    private readonly ILogger<HistoricalDataService> _logger;
    private readonly StatsContext _statsContext;
    private int lastProcessedGame = 100;
    
    private Dictionary<string, string> globalWeaponStats = new Dictionary<string, string>();
    private Dictionary<string, string> globalMapStats = new Dictionary<string, string>();
    private int globalKills = 0;
    private int globalDeaths = 0;

    public HistoricalDataService(ILogger<HistoricalDataService> logger, StatsContext statsContext)
    {
        _logger = logger;
        _statsContext = statsContext;
    }

    public async Task<Boolean> SyncFromLastProcessedGame()
    {
        HttpClient httpClient = new HttpClient();

        _logger.LogInformation("Retrieving Historical Stats starting from match ID " + lastProcessedGame);
        while (lastProcessedGame == 100)
        {
            _logger.LogInformation("Starting to parse for match " + lastProcessedGame);

            HttpResponseMessage response =
                await httpClient.GetAsync("http://glows.gg:8013/api/get_map_scoreboard?map_id=" + lastProcessedGame);

            if (response.IsSuccessStatusCode)
            {
                String responseString = await response.Content.ReadAsStringAsync();
                responseString = responseString.Replace("_", "");
                
                try
                {
                    ResultDto historicalMapStats = JsonConvert.DeserializeObject<ResultDto>(responseString);
                    if (historicalMapStats.result != null)
                    {
                        await ProcessStatsForMatch(historicalMapStats);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("Error deserializing stats... Aborting");
                    _logger.LogError(e.StackTrace);
                    return false;
                }

                lastProcessedGame++;
            }
        }
        _statsContext.SaveChanges();


        return true;
    }

    public async Task<Boolean> CreateJsonFile()
    {
        List < PlayerStats > playerStatsList = _statsContext.PlayerStats.ToList();
        
        using (StreamWriter file = File.CreateText(@"D:\data\path.txt"))
        {
            JsonSerializer serializer = new JsonSerializer();
            //serialize object directly into file stream
            serializer.Serialize(file, playerStatsList);
        }

        return true;
    }
    
    public async Task<Boolean> ReadJsonFile()
    {
        
        using (StreamReader r = new StreamReader("./Services/path.txt"))
        {
            string json = r.ReadToEnd();
            List<PlayerStats> items = JsonConvert.DeserializeObject<List<PlayerStats>>(json);
        }
    
        return true;
    }
    

    private async Task<PlayerStats> ProcessStatsForMatch(ResultDto mapStats)
    {
        GlobalStats globalStats = _statsContext.GlobalStats
            .OrderByDescending(p => p.id)
            .FirstOrDefault();

        if (globalStats == null)
        {
            globalStats = new GlobalStats();
        }
        List<PlayerDto> playerRecords = mapStats.result.playerStats;
        for (int i = 0; i < playerRecords.Count; i++)
        {
            PlayerDto playerToProcess = playerRecords[i];

            PlayerStats historicalStatsForPlayer =
                await _statsContext.FindAsync<PlayerStats>(playerToProcess.steamId64);
            PlayerStats processedStats;
            if (historicalStatsForPlayer == null)
            {
               processedStats = ProcessNewPlayerRecord(playerToProcess, mapStats.result.mapName, mapStats.result.start);
            }
            else
            {
                processedStats = AddStatsToExistingPlayerRecord(historicalStatsForPlayer, playerToProcess, mapStats.result.mapName,
                    mapStats.result.start);
            }

            UpdateGlobalStats(globalStats, processedStats);
        }
        return null;
    }

    private PlayerStats ProcessNewPlayerRecord(PlayerDto playerDto, String mapName, String matchStart)
    {
        PlayerStats playerStats = new PlayerStats();
        try
        {
            _logger.LogInformation("Adding new player record for " + playerDto.steamId64);
            if (playerDto.steamInfo != null)
            {
                _logger.LogInformation("Mapped " + playerDto.steamInfo.profile.personaName + " to " +
                                       playerDto.steamId64);
            }

            if (playerDto.steamId64.Equals("76561198042426888"))
            {
                Console.WriteLine("you");
            }

            

            Dictionary<String, String> mapStats = new Dictionary<string, string>();
            mapStats.Add(mapName, "1");
            playerStats.steamId = playerDto.steamId64;
            try
            {
                playerStats.gamertag =
                    playerDto.steamInfo != null ? playerDto.steamInfo.profile.personaName : "UNMAPPED";
            }
            catch (Exception e)
            {
                _logger.LogError("no gamertag in steam info");
            }

            playerStats.averageDpm = playerDto.deathsPerMinute;
            playerStats.averageKpm = playerDto.killsPerMinute;
            playerStats.averageKd = playerDto.killDeathRatio;
            playerStats.lastSeen = matchStart;
            playerStats.firstSeen = matchStart;
            playerStats.mostDeaths = playerDto.deaths;
            playerStats.totalDeaths = playerDto.deaths;
            playerStats.totalKills = playerDto.kills;
            playerStats.mostKills = playerDto.kills;
            playerStats.weaponKillsBlob = JsonConvert.SerializeObject(playerDto.weapons);
            playerStats.weaponDeathsBlob = JsonConvert.SerializeObject(playerDto.deathByWeapons);
            playerStats.mapBlob = JsonConvert.SerializeObject(mapStats);
            playerStats.timesBetrayed = playerDto.deathsByTk;
            playerStats.totalGames = 1;
            playerStats.highestKillStreak = playerDto.killsStreak;
            _statsContext.Add(playerStats);
        }
        catch(Exception e)
        {
            _logger.LogError("Error processing new player record for " + playerDto.steamId64);
            _logger.LogError(e.StackTrace);
        }
        return null;
    }

    private PlayerStats AddStatsToExistingPlayerRecord(PlayerStats existingStats, PlayerDto statsToAdd, String mapName,
        String matchStart)
    {
        if (statsToAdd.steamInfo != null)
        {
            _logger.LogInformation("Updating player record for " + statsToAdd.steamInfo.profile.personaName + " (" +
                                   statsToAdd.steamId64 + ")");
        }
        else
        {
            _logger.LogInformation("Updating player record for " + statsToAdd.steamId64);
        }
        existingStats.totalGames += 1;

        existingStats.lastSeen = matchStart;

        existingStats.totalDeaths += statsToAdd.deaths;
        existingStats.totalKills += statsToAdd.kills;


        existingStats.averageDpm = existingStats.averageDpm +
                                   ((statsToAdd.deathsPerMinute - existingStats.averageDpm) / existingStats.totalGames);
        existingStats.averageKpm = existingStats.averageKpm +
                                   ((statsToAdd.killsPerMinute - existingStats.averageKpm) / existingStats.totalGames);
        existingStats.averageKd = existingStats.averageKd +
                                  ((statsToAdd.killDeathRatio - existingStats.averageKd) / existingStats.totalGames);

        if (statsToAdd.deaths > existingStats.mostDeaths)
        {
            existingStats.mostDeaths = statsToAdd.deaths;
        }

        if (statsToAdd.kills > existingStats.mostKills)
        {
            existingStats.mostKills = statsToAdd.kills;
        }

        if (statsToAdd.killsStreak > existingStats.highestKillStreak)
        {
            existingStats.highestKillStreak = statsToAdd.killsStreak;
        }

        if (statsToAdd.deathsWithoutKillStreak > existingStats.highestDeathStreak)
        {
            existingStats.highestDeathStreak = statsToAdd.deathsWithoutKillStreak;
        }

        existingStats.weaponKillsBlob = UpdateWeaponStats(statsToAdd.weapons, existingStats.weaponKillsBlob);
        existingStats.weaponDeathsBlob = UpdateWeaponStats(statsToAdd.deathByWeapons, existingStats.weaponDeathsBlob);

        Dictionary<string, string> maps =
            JsonConvert.DeserializeObject<Dictionary<string, string>>(existingStats.mapBlob);

        if (maps.ContainsKey(mapName))
        {
            maps[mapName] = (Convert.ToInt32(maps[mapName]) + 1).ToString();
        }
        else
        {
            maps.Add(mapName, "1");
        }
        
        existingStats.mapBlob = JsonConvert.SerializeObject(maps);
        _statsContext.Update(existingStats);

        return existingStats;
    }

    private string UpdateWeaponStats(Dictionary<string, string> weaponsToAdd,
        string existingWeaponJsonBlob)
    {
        if (existingWeaponJsonBlob.Equals("null"))
        {
            existingWeaponJsonBlob = "{}";
        }
        Dictionary<string, string> currentWeaponStats =
            JsonConvert.DeserializeObject<Dictionary<string, string>>(existingWeaponJsonBlob);


        if (weaponsToAdd != null)
        {
            foreach (KeyValuePair<string, string> weapon in weaponsToAdd)
            {
                if (currentWeaponStats.ContainsKey(weapon.Key))
                {
                    currentWeaponStats[weapon.Key] =
                        (Convert.ToInt32(currentWeaponStats[weapon.Key]) + Convert.ToInt32(weapon.Value)).ToString();
                }
                else
                {
                    currentWeaponStats.Add(weapon.Key, weapon.Value);
                }
            }
        }

        return JsonConvert.SerializeObject(currentWeaponStats);
    }

    private void UpdateGlobalStats(GlobalStats globalStats, PlayerStats playerStats)
    {
        if (playerStats != null)
        {
            globalStats.totalDeaths += playerStats.totalDeaths;
            globalStats.totalKills += playerStats.totalKills;

            if (playerStats.highestKillStreak > globalStats.highestKillstreak)
            {
                globalStats.highestKillstreak = playerStats.highestKillStreak;
            }

            if (playerStats.mostKills > globalStats.highestKills)
            {
                globalStats.highestKillstreak = playerStats.highestKillStreak;
            }

            if (playerStats.highestKillStreak > globalStats.highestKillstreak)
            {
                globalStats.highestKillstreak = playerStats.highestKillStreak;
            }

            globalStats.highestKills += playerStats.totalDeaths;
        }
    }
}