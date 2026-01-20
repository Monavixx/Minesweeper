using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;
using Minesweeper.Core.Persistence;
using Minesweeper.UI.Screens;

namespace Minesweeper.UI.Commands.Game;

public class LoadGameCommand (Core.Game.Game game, IGameStateStore gameStateStore, int gameId) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.LoadGame(gameStateStore.Load(gameId));
        return InputHandleResult.NavigateTo<GameScreen>();
    }
}