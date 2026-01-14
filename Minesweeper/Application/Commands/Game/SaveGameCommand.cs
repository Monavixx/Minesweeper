using Minesweeper.Application.Input;
using Minesweeper.Application.Persistence;
using Minesweeper.Core.Game;

namespace Minesweeper.Application.Commands.Game;

public class SaveGameCommand (IGameStateStore gameStateStore, GameState gameState): ICommand
{
    public InputHandleResult? Execute()
    {
        gameStateStore.Save(gameState);
        return InputHandleResult.None();
    }
}