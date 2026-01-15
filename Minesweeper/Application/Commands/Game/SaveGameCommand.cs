using Minesweeper.Application.Input;
using Minesweeper.Application.Persistence;
using Minesweeper.Core.Game;
using Minesweeper.Core.Persistence;

namespace Minesweeper.Application.Commands.Game;

public class SaveGameCommand (IGameStateStore gameStateStore, GameState gameState): ICommand
{
    public InputHandleResult? Execute()
    {
        gameState.Pause();
        gameStateStore.Save(gameState);
        gameState.Resume();
        return InputHandleResult.None();
    }
}