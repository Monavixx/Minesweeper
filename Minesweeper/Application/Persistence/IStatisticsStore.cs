using Minesweeper.Core.Statistics;

namespace Minesweeper.Application.Persistence;

public interface IStatisticsStore
{
    void Save(Statistics statistics);
    Statistics LoadOrDefault();
}