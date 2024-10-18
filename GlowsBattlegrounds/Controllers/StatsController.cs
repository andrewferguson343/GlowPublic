using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GlowsBattlegrounds.CustomModels;
using GlowsBattlegrounds.Models;
using GlowsBattlegrounds.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace GlowsBattlegrounds.Controllers;

[ApiController]
[Route("[controller]")]
public class StatsController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<StatsController> _logger;
    private readonly IHistoricalDataService _historicalDataService;
    private readonly IStatsService _statsService;
    
    public StatsController(ILogger<StatsController> logger, IHistoricalDataService historicalDataService, IStatsService statsService)
    {
        _logger = logger;
        _historicalDataService = historicalDataService;
        _statsService = statsService;
    }

    [HttpGet("player/{playerId}")]
    public JsonResult getStatsById(string playerId)
    {
        PlayerStats playerStats = _statsService.GetStatsBySteamId(playerId);
        if(playerStats == null)
        {
            _logger.LogInformation("Player not found in database, returning 404");
            NotFound();
        }
        else
        {
            _logger.LogInformation("Player found in database, returning stats");
            
        }

        return new JsonResult(playerStats);
    }
    // [HttpGet("player/{gamertag}")]
    // public async Task getStatsByGamertag()
    // {
    //     HttpClient httpClient = new HttpClient();
    //
    //     HttpResponseMessage response = await httpClient.GetAsync("http://glows.gg:8013/api/get_map_scoreboard?map_id=30000");
    //
    //     String responseString = await response.Content.ReadAsStringAsync();
    //     responseString = responseString.Replace("_", "");
    //     var testTask =await _historicalDataService.SyncFromLastProcessedGame();
    // }
}