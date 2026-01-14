using Minesweeper.Application.Input;
using Minesweeper.Application.Screens.Menu;

namespace Minesweeper.Application.Commands.Menu;

public class PreviosMenuOptionCommand (IMenuContext menuContext) : ICommand
{
    public InputHandleResult? Execute()
    {
        --menuContext.SelectedIndex;
        return InputHandleResult.None();
    }
}