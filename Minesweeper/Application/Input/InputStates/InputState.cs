using Minesweeper.Application.Commands;

namespace Minesweeper.Application.Input.InputStates;

public class InputState : IInputState
{
    private readonly Dictionary<ConsoleKey, Func<ICommand>> _commands = new();
    public InputHandleResult? HandleInput(ConsoleKeyInfo keyInfo)
    {
        return _commands.TryGetValue(keyInfo.Key, out var func) ? func().Execute() : null;
    }
    protected void RegisterCommand(ConsoleKey key, Func<ICommand> command)
    {
        _commands.Add(key, command);
    }
}