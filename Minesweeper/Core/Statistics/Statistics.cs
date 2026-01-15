namespace Minesweeper.Core.Statistics;

public class Statistics
{
    public event Action? Changed;

    public uint GamesPlayed { get; set; }

    public uint VictoriesPlayed { get; set; }

    public uint GameOversPlayed { get; set; }

    public TimeSpan TotalTimePlayed { get; set; }

    public void Commit()
    {
        Changed?.Invoke();
    }
    // TODO: add average time stat.
    public IEnumerable<string> GetStatistics()
    {
        yield return $"Total Games Played: {GamesPlayed}";
        yield return $"Game Overs: {GameOversPlayed}";
        yield return $"Victories Played: {VictoriesPlayed}";
        yield return $"Total Time Played: {TotalTimePlayed}";
        yield return $"Average Time Played: {AveragePlayTime.TotalSeconds}s";
    }
    public TimeSpan AveragePlayTime => GamesPlayed == 0 ? TimeSpan.Zero : TotalTimePlayed.Divide(GamesPlayed);
}