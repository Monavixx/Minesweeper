using Minesweeper.Application.Cursor;
using Minesweeper.Application.Input;

namespace Minesweeper.Application.Commands.Game;
using Minesweeper.Core.Game;

public class ToggleFlaggedCommand (Game game, CursorState cursorState) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.ToggleFlagged(cursorState.X, cursorState.Y);
        return InputHandleResult.None();
    }
}