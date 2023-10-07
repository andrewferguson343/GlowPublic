using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace GlowsBattlegrounds.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("canary")]
    public async void Login()
    {
        HttpRequestMessage request = new HttpRequestMessage();
        HttpClient httpClient = new HttpClient();

        HttpResponseMessage response = await httpClient.GetAsync("http://glows.gg:8013/api/is_logged_in");
        
        
        var responseString = await response.Content.ReadAsStringAsync();
    }
}