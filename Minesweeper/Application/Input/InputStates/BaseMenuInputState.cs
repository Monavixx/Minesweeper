using Minesweeper.Application.Commands.Menu;
using Minesweeper.Application.Screens.Menu;

namespace Minesweeper.Application.Input.InputStates;

public class BaseMenuInputState : InputState
{
    public BaseMenuInputState(IMenuContext menuContext)
    {
        RegisterCommand(ConsoleKey.DownArrow, () => new NextMenuOptionCommand(menuContext));
        RegisterCommand(ConsoleKey.UpArrow, () => new PreviosMenuOptionCommand(menuContext));
        RegisterCommand(ConsoleKey.Enter, () => new ExecuteSelectedMenuOptionCommand(menuContext));
    }
}