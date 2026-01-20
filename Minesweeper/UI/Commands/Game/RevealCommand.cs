using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;
using Minesweeper.UI.Cursor;

namespace Minesweeper.UI.Commands.Game;

public class RevealCommand(Core.Game.Game game, CursorState cursorState) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.Reveal(cursorState.X, cursorState.Y);
        return InputHandleResult.None();
    }
}