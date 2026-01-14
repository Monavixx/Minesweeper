using Minesweeper.Application.Input;
using Minesweeper.Application.Persistence;
using Minesweeper.Application.Screens;
using Minesweeper.Application.Services;
using Minesweeper.Application.Viewport;
using Minesweeper.Core.Game;
using Minesweeper.Core.Generation;

namespace Minesweeper.Application;

public class Application
{
    private readonly IGameStateStore _gameStateStore;
    private readonly AppNavigator _navigator;
    private readonly GlobalInputManager _globalInputManager;
    private readonly Game _game;
    private readonly IViewport _viewport;
    private readonly ScreenFactory _screenFactory;
    private readonly StatisticsManager _statisticsManager;


    private bool _isRunning = true;

    public Application()
    {
        var appPaths = new AppPaths(Directory.GetCurrentDirectory());
        IBoardConfigStore boardConfigStore = new JsonBoardConfigStore(appPaths);
        var boardConfig = boardConfigStore.LoadOrCreateDefault();
        _gameStateStore = new JsonGameStateStore(appPaths);
        
        _game = new Game(new BoardGenerator(), boardConfig);
        _viewport = new ConsoleViewport();
        _screenFactory = new ScreenFactory();
        RegisterScreens();
        _navigator = new AppNavigator()
        {
            CurrentScreen = _screenFactory.Create<MainMenuScreen>(_viewport),
            ScreenFactory = _screenFactory,
            Viewport = _viewport,
        };
        _globalInputManager = new GlobalInputManager();
        
        _statisticsManager = new StatisticsManager(new JsonStatisticsStore(appPaths), _game);
    }

    private void RegisterScreens()
    {
        _screenFactory.Register<GameScreen>(viewport => 
            new GameScreen(viewport, _game, _gameStateStore, _statisticsManager.Statistics));
        _screenFactory.Register<MainMenuScreen>( viewport => new MainMenuScreen(viewport, _game));
        _screenFactory.Register<LoadGameMenuScreen>(viewport => 
            new LoadGameMenuScreen(viewport, _game, _gameStateStore));
    }
    
    public void Run()
    {
        Console.CursorVisible = false;
        while (_isRunning)
        {
            _viewport.Update();
            _navigator.CurrentScreen.Render();
            var key = Console.ReadKey(true);
            var inputHandleResult = _navigator.CurrentScreen.HandleInput(key)
                      ?? _globalInputManager.HandleInput(key);
            if (inputHandleResult != null)
                switch (inputHandleResult.Action)
                {
                    case InputActionType.Exit:
                        _isRunning = false;
                        break;
                    case InputActionType.NavigateTo:
                        _navigator.NavigateTo(inputHandleResult.TargetScreenType!);
                        break;
                }
        }
        // Final render
        _navigator.CurrentScreen.Render();
    }
}