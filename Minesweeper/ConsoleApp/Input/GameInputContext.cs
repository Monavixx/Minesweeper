using Minesweeper.ConsoleApp.Commands;
using Minesweeper.Core.Commands;

namespace Minesweeper.ConsoleApp.Input;

public class GameInputContext (ConsoleAppState consoleAppState) : IInputContext
{
    private Dictionary<ConsoleKey, Func<ICommand>> _commands = new()
    {
        { ConsoleKey.DownArrow, () => new MoveCursorCommand(consoleAppState, MoveDirection.Down) },
        { ConsoleKey.UpArrow, () => new MoveCursorCommand(consoleAppState, MoveDirection.Up) },
        { ConsoleKey.LeftArrow, () => new MoveCursorCommand(consoleAppState, MoveDirection.Left) },
        { ConsoleKey.RightArrow, () => new MoveCursorCommand(consoleAppState, MoveDirection.Right) },
        { ConsoleKey.F, () => new ToggleFlaggedCommand(consoleAppState.Game,
                consoleAppState.CursorState.X, consoleAppState.CursorState.Y) },
        { ConsoleKey.Enter, () => new RevealCommand(consoleAppState.Game, 
            consoleAppState.CursorState.X, consoleAppState.CursorState.Y) },
    };
    
    public ICommand? Resolve(ConsoleKeyInfo keyInfo)
    {
        if (_commands.TryGetValue(keyInfo.Key, out var func))
        {
            return func();
        }

        return null;
    }
}