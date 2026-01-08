using Minesweeper.Core.Commands;

namespace Minesweeper.ConsoleApp.Commands;

public class MoveCursorCommand(ConsoleAppState consoleAppState, MoveDirection moveDirection) : ICommand
{
    public void Execute()
    {
        consoleAppState.MoveCursor(moveDirection);
    }
}
