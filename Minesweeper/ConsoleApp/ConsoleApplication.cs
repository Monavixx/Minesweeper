using Minesweeper.ConsoleApp.Input;
using Minesweeper.ConsoleApp.Persistence;
using Minesweeper.ConsoleApp.Render;
using Minesweeper.Core;
using Minesweeper.Core.Game;
using Minesweeper.Core.Generation;
using Minesweeper.Core.Statistics;

namespace Minesweeper.ConsoleApp;

public sealed class ConsoleApplication
{
    private readonly ConsoleAppState _consoleAppState;
    private readonly RenderManager _renderManager;
    private readonly InputManager _inputManager;
    private readonly StatisticsManager _statsManager;
    private readonly AppPaths _appPaths;

    public ConsoleApplication()
    {
        _appPaths = new AppPaths(Directory.GetCurrentDirectory());
        _statsManager = new StatisticsManager(new JsonStatisticsStore(_appPaths));
        
        _consoleAppState = new ConsoleAppState(
            appState: new AppState()
            {
                BoardConfig = new BoardConfig()
                {
                    Width = 20,
                    Height = 10,
                    MineChance = 10
                }
            },
            game: new Game(new BoardGenerator()),
            statistics: _statsManager.Statistics
        );
        _consoleAppState.BindStatisticsToEvents();
        
        _renderManager = new RenderManager(new ConsolePartialRenderer(_consoleAppState));
        _renderManager.RenderContext = new MainMenuRenderContext();
        _inputManager = new InputManager(_consoleAppState);
        _inputManager.CurrentContext = new GameOverInputContext(_consoleAppState);
        
        _consoleAppState.Game.OnGameOver += HandleGameOver;
        _consoleAppState.Game.OnVictory += HandleGameOver;
        _consoleAppState.Game.OnGameStarted += HandleGameStarted;
    }
    public void Run()
    {
        while (_consoleAppState.IsRunning)
        {
            _renderManager.Render();
            
            _inputManager.HandleInput(Console.ReadKey(true));
        }
    }

    private void HandleGameOver()
    {
        _inputManager.CurrentContext = new GameOverInputContext(_consoleAppState);
    }

    private void HandleGameStarted()
    {
        _inputManager.CurrentContext = new GameInputContext(_consoleAppState);
        _consoleAppState.CursorState.X = 0;
        _consoleAppState.CursorState.Y = 0;
        _renderManager.RenderContext = new GameRenderContext();
    }
}