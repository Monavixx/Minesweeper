using System.Diagnostics.CodeAnalysis;
using Minesweeper.Application.Commands;
using Minesweeper.Application.Commands.Game;
using Minesweeper.Application.Commands.MainMenu;
using Minesweeper.Application.Render;
using Minesweeper.Application.Screens.Menu;
using Minesweeper.Application.Viewport;
using Minesweeper.Core.Game;

namespace Minesweeper.Application.Screens;

public class MainMenuScreen : BaseMenuScreen
{
    private readonly Game _game;
    [SetsRequiredMembers]
    public MainMenuScreen(IViewport viewport, Game game) : base(viewport)
    {
        _game = game;
        Init();
    }
    [SetsRequiredMembers]
    public MainMenuScreen(IViewport viewport, Game game, BaseMenuRenderer baseMenuRenderer) : base(viewport, baseMenuRenderer)
    {
        _game = game;
        Init();
    }
    
    private void Init() 
    {
        /*RegisterOption("One", ()=>new ExitCommand());
        RegisterOption("Two", ()=>new ExitCommand());
        RegisterOption("Three", ()=>new ExitCommand());
        RegisterOption("Four", ()=>new ExitCommand());
        RegisterOption("Five", ()=>new ExitCommand());
        RegisterOption("Six", ()=>new ExitCommand());
        RegisterOption("Seven", ()=>new ExitCommand());
        RegisterOption("Eight", ()=>new ExitCommand());
        RegisterOption("Nine", ()=>new ExitCommand());*/
        RegisterOption("New game", () => new StartNewGameCommand(_game));
        RegisterOption("Load game", () => new OpenLoadGameMenuCommand());
        RegisterOption("Exit", () => new ExitCommand());
    }
}