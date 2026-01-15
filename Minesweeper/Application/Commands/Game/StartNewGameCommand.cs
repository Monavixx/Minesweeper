using Minesweeper.Application.Input;
using Minesweeper.Application.Screens;
using Minesweeper.Core.Persistence;

namespace Minesweeper.Application.Commands.Game;
using Minesweeper.Core.Game;
public class StartNewGameCommand (Game game, IGameStateStore gameStateStore) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.StartNewGame(gameStateStore);
        return InputHandleResult.NavigateTo<GameScreen>();
    }
}