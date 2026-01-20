using System.Diagnostics.CodeAnalysis;
using ConsoleGameFramework.Render;
using ConsoleGameFramework.Screens.Menu;
using ConsoleGameFramework.Viewport;
using Minesweeper.Core.Game;
using Minesweeper.Core.Persistence;
using Minesweeper.UI.Commands.Game;

namespace Minesweeper.UI.Screens;

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