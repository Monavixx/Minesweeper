using ConsoleGameFramework;
using ConsoleGameFramework.Viewport;
using Minesweeper.Core.Game;
using Minesweeper.Core.Generation;
using Minesweeper.Core.Persistence;
using Minesweeper.UI.Persistence;
using Minesweeper.UI.Screens;
using Minesweeper.UI.Services;
using Minesweeper.UI.Time;

namespace Minesweeper.UI;

public class MinesweeperApplication
{
    private readonly IGameStateStore _gameStateStore;
    private readonly Game _game;
    private readonly IViewport _viewport;
    private readonly StatisticsManager _statisticsManager;
    
    public MinesweeperApplication()
    {
        var appPaths = new AppPaths(Directory.GetCurrentDirectory());
        var boardConfigStore = new JsonBoardConfigStore(appPaths);
        var boardConfig = boardConfigStore.LoadOrCreateDefault();
        _gameStateStore = new JsonGameStateStore(appPaths);

        var timeProvider = new UtcTimeProvider();
        _game = new Game(new BoardGenerator(), boardConfig, timeProvider);
        _viewport = new ConsoleViewport();
        
        _statisticsManager = new StatisticsManager(new JsonStatisticsStore(appPaths), _game);
    }
    
    public void Run()
    {
        var app = new Application(_viewport);
        app.RegisterScreens(sf =>
            {
                sf.Register<GameScreen>(viewport =>
                    new GameScreen(viewport, _game, _gameStateStore, _statisticsManager.Statistics));
                sf.Register<MainMenuScreen>(viewport => new MainMenuScreen(viewport, _game, _gameStateStore));
                sf.Register<LoadGameMenuScreen>(viewport =>
                    new LoadGameMenuScreen(viewport, _game, _gameStateStore));
            })
            .CurrentScreen<MainMenuScreen>()
            .Run();
    }
}