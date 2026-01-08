using Minesweeper.Core.Commands;

namespace Minesweeper.ConsoleApp.Commands;

public class ExitCommand (ConsoleAppState consoleAppState) : ICommand
{
    public void Execute()
    {
        consoleAppState.IsRunning = false;
    }
}