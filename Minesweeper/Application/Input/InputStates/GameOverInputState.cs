using Minesweeper.Application.Commands.Game;
using Minesweeper.Core.Game;
using Minesweeper.Core.Persistence;

namespace Minesweeper.Application.Input.InputStates;

public class GameOverInputState : InputState
{
    public GameOverInputState(Game game, IGameStateStore gameStateStore)
    {
        RegisterCommand(ConsoleKey.Enter, () => new StartNewGameCommand(game, gameStateStore));
    }
}