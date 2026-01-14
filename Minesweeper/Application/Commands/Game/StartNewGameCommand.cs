using Minesweeper.Application.Input;
using Minesweeper.Application.Screens;

namespace Minesweeper.Application.Commands.Game;
using Minesweeper.Core.Game;
public class StartNewGameCommand (Game game) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.StartNewGame();
        return InputHandleResult.NavigateTo<GameScreen>();
    }
}