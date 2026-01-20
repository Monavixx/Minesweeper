using System.Diagnostics.CodeAnalysis;
using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Render;
using ConsoleGameFramework.Screens.Menu;
using ConsoleGameFramework.Viewport;
using Minesweeper.Core.Game;
using Minesweeper.Core.Persistence;
using Minesweeper.UI.Commands.Game;
using Minesweeper.UI.Commands.MainMenu;

namespace Minesweeper.UI.Screens;

public class MainMenuScreen : BaseMenuScreen
{
    private readonly Game _game;
    private readonly IGameStateStore _gameStateStore;
    [SetsRequiredMembers]
    public MainMenuScreen(IViewport viewport, Game game, IGameStateStore gameStateStore) : base(viewport)
    {
        _game = game;
        _gameStateStore = gameStateStore;
        Init();
    }
    [SetsRequiredMembers]
    public MainMenuScreen(IViewport viewport, Game game, IGameStateStore gameStateStore, BaseMenuRenderer baseMenuRenderer) : base(viewport, baseMenuRenderer)
    {
        _game = game;
        _gameStateStore = gameStateStore;
        Init();
    }
    
    private void Init() 
    {
        // RegisterOption("One", ()=>new ExitCommand());
        // RegisterOption("Two", ()=>new ExitCommand());
        // RegisterOption("Three", ()=>new ExitCommand());
        // RegisterOption("Four", ()=>new ExitCommand());
        // RegisterOption("Five", ()=>new ExitCommand());
        // RegisterOption("Six", ()=>new ExitCommand());
        // RegisterOption("Seven", ()=>new ExitCommand());
        // RegisterOption("Eight", ()=>new ExitCommand());
        // RegisterOption("Nine", ()=>new ExitCommand());
        RegisterOption("New game", () => new StartNewGameCommand(_game, _gameStateStore));
        RegisterOption("Load game", () => new OpenLoadGameMenuCommand());
        RegisterOption("Exit", () => new ExitCommand());
    }
}