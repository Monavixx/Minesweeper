using Minesweeper.Core.Game;
using Minesweeper.Core.Statistics;
using Minesweeper.UI.Persistence;

namespace Minesweeper.UI.Services;

public class StatisticsManager : IDisposable
{
    private readonly IStatisticsStore _statisticsStore;
    public Statistics Statistics { get; }
    private readonly Game _game;

    public StatisticsManager(IStatisticsStore statisticsStore, Game game)
    {
        _game = game;
        _statisticsStore = statisticsStore;
        Statistics = _statisticsStore.LoadOrDefault();
        Statistics.Changed += OnStatisticsChanged;
        
        game.OnGameOver += HandleGameOver;
        game.OnVictory += HandleVictory;
    }
    private void OnStatisticsChanged()
    {
        _statisticsStore.Save(Statistics);
    }

    private void HandleGameOver()
    {
        ++Statistics.GameOversPlayed;
        HandleGameEnded();
    }

    private void HandleVictory()
    {
        ++Statistics.VictoriesPlayed;
        HandleGameEnded();
    }

    private void HandleGameEnded()
    {
        ++Statistics.GamesPlayed;
        Statistics.TotalTimePlayed += _game.GameState.AccumulatedPlayTime;
        Statistics.Commit();
    }

    public void Dispose()
    {
        _game.OnGameOver -= HandleGameOver;
        _game.OnVictory -= HandleVictory;
    }
}