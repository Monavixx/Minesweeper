namespace Minesweeper.Core.Commands;
using Game;

public class RevealCommand(Game game, int x, int y) : ICommand
{
    public void Execute()
    {
        game.Reveal(x, y);
    }
}