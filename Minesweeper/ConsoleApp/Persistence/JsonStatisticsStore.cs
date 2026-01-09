using System.Text.Json;
using Minesweeper.Core.Statistics;
using Minesweeper.Core.Store;

namespace Minesweeper.ConsoleApp.Persistence;

public class JsonStatisticsStore : IStatisticsStore
{
    private readonly AppPaths _appPaths;

    public JsonStatisticsStore(AppPaths appPaths)
    {
        _appPaths = appPaths;
    }

    public void Save(Statistics statistics)
    {
        string json = JsonSerializer.Serialize(statistics);
        File.WriteAllText(_appPaths.StatisticsFile, json);
    }

    public Statistics? Load()
    {
        if(!File.Exists(_appPaths.StatisticsFile)) return null;
        
        string json = File.ReadAllText(_appPaths.StatisticsFile);
        return JsonSerializer.Deserialize<Statistics>(json);
    }
}