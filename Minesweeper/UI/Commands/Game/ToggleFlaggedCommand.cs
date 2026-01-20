using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;
using Minesweeper.UI.Cursor;

namespace Minesweeper.UI.Commands.Game;

public class ToggleFlaggedCommand (Core.Game.Game game, CursorState cursorState) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.ToggleFlagged(cursorState.X, cursorState.Y);
        return InputHandleResult.None();
    }
}