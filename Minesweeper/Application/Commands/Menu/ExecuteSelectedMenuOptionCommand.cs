using Minesweeper.Application.Input;
using Minesweeper.Application.Screens.Menu;

namespace Minesweeper.Application.Commands.Menu;

public class ExecuteSelectedMenuOptionCommand (IMenuContext menuContext): ICommand
{
    public InputHandleResult? Execute()
    {
        return menuContext.ExecuteSelected();
    }
}