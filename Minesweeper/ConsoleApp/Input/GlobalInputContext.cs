using Minesweeper.ConsoleApp.Commands;
using Minesweeper.Core.Commands;

namespace Minesweeper.ConsoleApp.Input;

public class GlobalInputContext(ConsoleAppState consoleAppState) : IInputContext
{
    public ICommand? Resolve(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Z)
        {
            return new ExitCommand(consoleAppState);
        }

        return null;
    }
}