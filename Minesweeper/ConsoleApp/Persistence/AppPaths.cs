namespace Minesweeper.ConsoleApp.Persistence;

public class AppPaths
{
    public string BaseDirectory { get; }
    public string StatisticsFile { get; }

    public AppPaths(string baseDirectory)
    {
        BaseDirectory = baseDirectory;
        StatisticsFile = Path.Combine(baseDirectory, "statistics.json");
    }
}