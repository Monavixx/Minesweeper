using Minesweeper.Application.Cursor;
using Minesweeper.Application.Input;

namespace Minesweeper.Application.Commands.Game;

public class RevealCommand(Core.Game.Game game, CursorState cursorState) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.Reveal(cursorState.X, cursorState.Y);
        return InputHandleResult.None();
    }
}