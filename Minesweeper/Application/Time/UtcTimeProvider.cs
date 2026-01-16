using Minesweeper.Core.Time;

namespace Minesweeper.Application.Time;

public class UtcTimeProvider : ITimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}