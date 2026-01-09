using Minesweeper.Core.Store;

namespace Minesweeper.Core.Statistics;

public class StatisticsManager
{
    private readonly IStatisticsStore _statisticsStore;
    public Statistics Statistics { get; private set; }

    public StatisticsManager(IStatisticsStore statisticsStore)
    {
        _statisticsStore = statisticsStore;
        LoadStatistics();
    }
    private void LoadStatistics()
    {
        Statistics = _statisticsStore.Load() ?? new Statistics();
        Statistics.Changed += OnStatisticsChanged;
    }

    private void OnStatisticsChanged()
    {
        _statisticsStore.Save(Statistics);
    }
}