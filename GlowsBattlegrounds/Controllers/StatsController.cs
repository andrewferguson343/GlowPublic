using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GlowsBattlegrounds.CustomModels;
using GlowsBattlegrounds.Models;
using GlowsBattlegrounds.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    
    public StatsController(ILogger<StatsController> logger, IHistoricalDataService historicalDataService)
    {
        _logger = logger;
        _historicalDataService = historicalDataService;
    }

    [HttpGet("player/{playerId}")]
    public async Task getStatsById()
    {
        HttpClient httpClient = new HttpClient();

        HttpResponseMessage response = await httpClient.GetAsync("http://glows.gg:8013/api/get_map_scoreboard?map_id=30000");

        String responseString = await response.Content.ReadAsStringAsync();
        responseString = responseString.Replace("_", "");
        var testTask =await _historicalDataService.SyncFromLastProcessedGame();
    }
    [HttpGet("player/{gamertag}")]
    public async Task getStatsByGamertag()
    {
        HttpClient httpClient = new HttpClient();

        HttpResponseMessage response = await httpClient.GetAsync("http://glows.gg:8013/api/get_map_scoreboard?map_id=30000");

        String responseString = await response.Content.ReadAsStringAsync();
        responseString = responseString.Replace("_", "");
        var testTask =await _historicalDataService.SyncFromLastProcessedGame();
    }
}