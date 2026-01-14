using Minesweeper.Application.Commands.Game;
using Minesweeper.Core.Game;

namespace Minesweeper.Application.Input.InputStates;

public class GameOverInputState : InputState
{
    public GameOverInputState(Game game)
    {
        RegisterCommand(ConsoleKey.Enter, () => new StartNewGameCommand(game));
    }
}