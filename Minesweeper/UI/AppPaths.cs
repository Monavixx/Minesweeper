namespace Minesweeper.UI;

public class AppPaths
{
    public string BaseDirectory { get; }
    public string StatisticsFile { get; }
    public string GamesDirectory { get; }
    public string GameFileExtension => "json";
    public string BoardConfigFile { get; }

    public AppPaths(string baseDirectory)
    {
        BaseDirectory = baseDirectory;
        StatisticsFile = Path.Combine(baseDirectory, "statistics.json");
        GamesDirectory = Path.Combine(baseDirectory, "games");
        BoardConfigFile = Path.Combine(baseDirectory, "board_config.json");
    }
}