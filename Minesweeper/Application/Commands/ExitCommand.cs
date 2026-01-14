using Minesweeper.Application.Input;

namespace Minesweeper.Application.Commands;

public class ExitCommand : ICommand
{
    public InputHandleResult? Execute()
    {
        return InputHandleResult.Exit();
    }
}