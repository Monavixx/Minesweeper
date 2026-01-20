using Minesweeper.Core.Statistics;

namespace Minesweeper.UI.Persistence;

public interface IStatisticsStore
{
    void Save(Statistics statistics);
    Statistics LoadOrDefault();
}