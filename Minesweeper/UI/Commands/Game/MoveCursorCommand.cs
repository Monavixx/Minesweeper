using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;
using Minesweeper.Core.Board;
using Minesweeper.UI.Cursor;

namespace Minesweeper.UI.Commands.Game;

public class MoveCursorCommand(Board board, CursorState cursor, MoveDirection moveDirection) : ICommand
{
    public InputHandleResult? Execute()
    {
        cursor.Move(moveDirection, board.Width, board.Height);
        return InputHandleResult.None();
    }
}
