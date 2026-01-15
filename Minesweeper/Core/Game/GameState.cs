using System.Text.Json.Serialization;

namespace Minesweeper.Core.Game;

using Board;

public class GameState(Board board)
{
    public Board Board { get; private set; } = board;
    public CurrentState State { get; set; }
    public TimeSpan AccumulatedPlayTime { get; set; }
    [JsonIgnore] private DateTime? LastResumedAtUtc { get; set; }

    public void Resume()
    {
        if (LastResumedAtUtc.HasValue)
            return;
        LastResumedAtUtc = DateTime.UtcNow;
    }

    public void Pause()
    {
        if (LastResumedAtUtc is null) return;
        AccumulatedPlayTime += DateTime.UtcNow - LastResumedAtUtc.Value;
        LastResumedAtUtc = null;
    }

    public enum CurrentState
    {
        Playing,
        GameOver,
        Victory
    }
    public required int GameId { get; init; }

    public TimeSpan TotalPlayTime => LastResumedAtUtc is null
        ? AccumulatedPlayTime
        : DateTime.UtcNow - LastResumedAtUtc.Value + AccumulatedPlayTime;
}