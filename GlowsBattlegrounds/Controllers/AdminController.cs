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
public class AdminController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<AdminController> _logger;
    private readonly IHistoricalDataService _historicalDataService;
    
    public AdminController(ILogger<AdminController> logger, IHistoricalDataService historicalDataService)
    {
        _logger = logger;
        _historicalDataService = historicalDataService;
    }

    [HttpGet("Canary")]
    public async Task CheckAuthenticated()
    {
        HttpClient httpClient = new HttpClient();

        HttpResponseMessage response = await httpClient.GetAsync("http://glows.gg:8013/api/get_map_scoreboard?map_id=1");

        String responseString = await response.Content.ReadAsStringAsync();
        responseString = responseString.Replace("_", "");
        var testTask = await _historicalDataService.SyncFromLastProcessedGame();
    }
    //
    // [HttpGet("json")]
    // public async Task WriteToJsonFile()
    // {
    //     var testTask =await _historicalDataService.CreateJsonFile();
    // }
    //
    // [HttpGet("readjson")]
    // public async Task ReadToJsonFile()
    // {
    //     var testTask =await _historicalDataService.ReadJsonFile();
    // }
    
}