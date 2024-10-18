using GlowsBattlegrounds.Models;

namespace GlowsBattlegrounds.Services;

public interface IStatsService
{
    PlayerStats GetStatsBySteamId(string steamId);
}