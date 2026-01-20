using Minesweeper.Core.Time;

namespace Minesweeper.UI.Time;

public class UtcTimeProvider : ITimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}