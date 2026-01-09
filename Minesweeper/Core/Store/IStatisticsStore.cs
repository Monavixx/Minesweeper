namespace Minesweeper.Core.Store;
using Statistics;
public interface IStatisticsStore
{
    void Save(Statistics statistics);
    Statistics? Load();
}