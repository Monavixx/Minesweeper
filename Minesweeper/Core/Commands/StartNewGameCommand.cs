using Minesweeper.Core.Generation;

namespace Minesweeper.Core.Commands;
using Game;

public class StartNewGameCommand (Game game, BoardConfig boardConfig) : ICommand
{
    public void Execute()
    {
        game.StartNewGame(boardConfig);
    }
}