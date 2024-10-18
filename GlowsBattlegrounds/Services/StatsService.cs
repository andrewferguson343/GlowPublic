using GlowsBattlegrounds.Models;

namespace GlowsBattlegrounds.Services;

public class StatsService : IStatsService
{
    private readonly ILogger<StatsService> _logger;

    private readonly StatsContext _statsContext;

    public StatsService(ILogger<StatsService> logger, StatsContext statsContext)
    {
        _logger = logger;
        _statsContext = statsContext;
    }
    public PlayerStats GetStatsBySteamId(string steamId)
    {
        PlayerStats historicalStatsForPlayer =
             _statsContext.Find<PlayerStats>(steamId);

        return historicalStatsForPlayer;
    }
}