namespace Minesweeper.Core.Commands;
using Game;

public class ToggleFlaggedCommand (Game game, int x, int y) : ICommand
{
    public void Execute()
    {
        game.ToggleFlagged(x, y);
    }
}