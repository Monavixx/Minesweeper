using System.Text.Json;
using Minesweeper.Core.Statistics;

namespace Minesweeper.Application.Persistence;

public class JsonStatisticsStore(AppPaths appPaths) : IStatisticsStore
{
    public void Save(Statistics statistics)
    {
        string json = JsonSerializer.Serialize(statistics);
        File.WriteAllText(appPaths.StatisticsFile, json);
    }

    public Statistics LoadOrDefault()
    {
        if(!File.Exists(appPaths.StatisticsFile)) return new Statistics();
        
        string json = File.ReadAllText(appPaths.StatisticsFile);

        try
        {
            return JsonSerializer.Deserialize<Statistics>(json) ?? new Statistics();
        }
        catch (JsonException)
        {
            return new Statistics();
        }
    }
}