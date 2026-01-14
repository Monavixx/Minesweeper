using Minesweeper.Application.Input;
using Minesweeper.Application.Screens;

namespace Minesweeper.Application.Commands.MainMenu;

public class OpenLoadGameMenuCommand : ICommand
{
    public InputHandleResult? Execute()
    {
        return InputHandleResult.NavigateTo<LoadGameMenuScreen>();
    }
}