namespace GlowsBattlegrounds.Services;

public interface IHistoricalDataService
{
    Task<bool> SyncFromLastProcessedGame();
    // Task<bool> CreateJsonFile();
    // Task<bool> ReadJsonFile();
}