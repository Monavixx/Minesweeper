using Minesweeper.Application.Cursor;
using Minesweeper.Application.Input;
using Minesweeper.Core.Board;

namespace Minesweeper.Application.Commands.Game;

public class MoveCursorCommand(Board board, CursorState cursor, MoveDirection moveDirection) : ICommand
{
    public InputHandleResult? Execute()
    {
        cursor.Move(moveDirection, board.Width, board.Height);
        return InputHandleResult.None();
    }
}
