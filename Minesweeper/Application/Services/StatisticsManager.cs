using Minesweeper.Application.Persistence;
using Minesweeper.Core.Game;
using Minesweeper.Core.Statistics;

namespace Minesweeper.Application.Services;

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
        
        game.OnNewGameStarted += HandleNewGameStarted;
        game.OnGameOver += HandleGameOver;
        game.OnVictory += HandleVictory;
    }
    private void OnStatisticsChanged()
    {
        _statisticsStore.Save(Statistics);
    }

    private void HandleNewGameStarted() => ++Statistics.GamesPlayed;
    private void HandleGameOver() => ++Statistics.GameOversPlayed;
    private void HandleVictory() => ++Statistics.VictoriesPlayed;

    public void Dispose()
    {
        _game.OnNewGameStarted -= HandleNewGameStarted;
        _game.OnGameOver -= HandleGameOver;
        _game.OnVictory -= HandleVictory;
    }
}