using Minesweeper.Core.Statistics;

namespace Minesweeper.Application.Render;

public class StatisticsRenderer : IDisposable
{
    private bool _fullRenderRequired = true;
    private readonly Statistics _statistics;

    public StatisticsRenderer(Statistics statistics)
    {
        _statistics = statistics;
        statistics.Changed += HandleStatisticsChanged;
    }
    private void HandleStatisticsChanged() => _fullRenderRequired = true;

    public void FullRender(int x, int y)
    {
        Console.BackgroundColor = ConsoleColor.Yellow | ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;

        List<string> stats = _statistics.GetStatistics().ToList();
        int statWidth = stats.Max(s => s.Length);
        foreach (var line in stats)
        {
            Console.SetCursorPosition(x, y++);
            Console.Write(line);
            Console.Write(new string (' ', statWidth-line.Length));
        }
        Console.ResetColor();

        _fullRenderRequired = false;
    }

    public void PartialRender(int x, int y)
    {
        if(_fullRenderRequired)
            FullRender(x, y);
    }

    public void Dispose()
    {
        _statistics.Changed -= HandleStatisticsChanged;
    }
}