using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;
using Minesweeper.Core.Persistence;
using Minesweeper.UI.Screens;

namespace Minesweeper.UI.Commands.Game;

public class StartNewGameCommand (Core.Game.Game game, IGameStateStore gameStateStore) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.StartNewGame(gameStateStore);
        return InputHandleResult.NavigateTo<GameScreen>();
    }
}