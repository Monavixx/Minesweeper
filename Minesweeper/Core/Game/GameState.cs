using System.Text.Json.Serialization;
using Minesweeper.Core.Time;

namespace Minesweeper.Core.Game;

using Board;

public class GameState(Board board, int gameId, ITimeProvider timeProvider)
{
    public Board Board { get; private set; } = board;
    public CurrentState State { get; set; }
    public TimeSpan AccumulatedPlayTime { get; set; }
    [JsonIgnore] private DateTime? LastResumedAtUtc { get; set; }
    public int GameId { get; } = gameId;

    public void Resume()
    {
        if (LastResumedAtUtc.HasValue)
            return;
        LastResumedAtUtc = timeProvider.UtcNow;
    }

    public void Pause()
    {
        if (LastResumedAtUtc is null) return;
        AccumulatedPlayTime += timeProvider.UtcNow - LastResumedAtUtc.Value;
        LastResumedAtUtc = null;
    }

    public enum CurrentState
    {
        Playing,
        GameOver,
        Victory
    }

    //public TimeSpan TotalPlayTime => LastResumedAtUtc is null
    //    ? AccumulatedPlayTime
    //    : timeProvider.UtcNow - LastResumedAtUtc.Value + AccumulatedPlayTime;
}