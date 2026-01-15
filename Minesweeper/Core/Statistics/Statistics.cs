namespace Minesweeper.Core.Statistics;

public class Statistics
{
    public event Action? Changed;

    public uint GamesPlayed
    {
        get;
        set
        {
            field = value;
            Changed?.Invoke();
        }
    }
    public uint VictoriesPlayed
    {
        get;
        set
        {
            field = value;
            Changed?.Invoke();
        }
    }
    public uint GameOversPlayed
    {
        get;
        set
        {
            field = value;
            Changed?.Invoke();
        }
    }
    // TODO: add average time stat.
    public IEnumerable<string> GetStatistics()
    {
        yield return $"Game Overs: {GameOversPlayed}";
        yield return $"Victories Played: {VictoriesPlayed}";
        yield return $"Games Played: {GamesPlayed}";
    }
}