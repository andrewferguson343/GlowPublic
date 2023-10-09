namespace GlowsBattlegrounds.Services;

public interface IHistoricalDataService
{
    Task<bool> SyncFromLastProcessedGame();
}