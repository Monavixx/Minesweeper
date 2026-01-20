using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;
using Minesweeper.UI.Screens;

namespace Minesweeper.UI.Commands.MainMenu;

public class OpenLoadGameMenuCommand : ICommand
{
    public InputHandleResult? Execute()
    {
        return InputHandleResult.NavigateTo<LoadGameMenuScreen>();
    }
}