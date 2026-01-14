using Minesweeper.Application.Commands;

namespace Minesweeper.Application.Input;

public class GlobalInputManager
{
    private readonly Dictionary<ConsoleKey, Func<ICommand>> _commands;

    public GlobalInputManager()
    {
        _commands = new ()
        {
            { ConsoleKey.Z, () => new ExitCommand() }
        };
    }

    public InputHandleResult? HandleInput(ConsoleKeyInfo key)
    {
        if (_commands.TryGetValue(key.Key, out var func))
        {
            return func().Execute();
        }

        return null;
    }
}