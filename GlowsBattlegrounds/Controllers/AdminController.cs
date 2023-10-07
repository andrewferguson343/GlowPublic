using System.Text;
using System.Text.Json.Serialization;
using GlowsBattlegrounds.CustomModels;
using Microsoft.AspNetCore.Mvc;

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
    
    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

    [HttpGet("canary")]
    public async void CheckAuthenticated()
    {
        HttpRequestMessage request = new HttpRequestMessage();
        HttpClient httpClient = new HttpClient();

        HttpResponseMessage response = await httpClient.GetAsync("http://glows.gg:8013/api/get_map_scoreboard?map_id=30000");

        String responseString = await response.Content.ReadAsStringAsync();
        
        responseString.re
    }
}