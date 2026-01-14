using System.Diagnostics.CodeAnalysis;
using Minesweeper.Application.Commands;
using Minesweeper.Application.Commands.MainMenu;
using Minesweeper.Application.Persistence;
using Minesweeper.Application.Render;
using Minesweeper.Application.Screens.Menu;
using Minesweeper.Application.Viewport;
using Minesweeper.Core.Game;

namespace Minesweeper.Application.Screens;

public class LoadGameMenuScreen : BaseMenuScreen
{
    
    [SetsRequiredMembers]
    public LoadGameMenuScreen(IViewport viewport, Game game, IGameStateStore gameStateStore) : base(viewport)
    {
        Init(game, gameStateStore);
    }

    [SetsRequiredMembers]
    public LoadGameMenuScreen(IViewport viewport, Game game, IGameStateStore gameStateStore, BaseMenuRenderer baseMenuRenderer) : base(viewport, baseMenuRenderer)
    {
        Init(game, gameStateStore);
    }

    private void Init(Game game, IGameStateStore gameStateStore)
    {
        foreach (var gameId in gameStateStore.FindAllGameIds())
        {
            RegisterOption($"#{gameId}", () => new LoadGameCommand(game, gameStateStore, gameId));
        }
    }
}