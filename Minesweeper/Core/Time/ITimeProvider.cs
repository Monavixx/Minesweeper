namespace Minesweeper.Core.Time;

public interface ITimeProvider
{
    public DateTime UtcNow { get; }
}