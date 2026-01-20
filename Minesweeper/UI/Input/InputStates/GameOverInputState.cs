using ConsoleGameFramework.Input.InputStates;
using Minesweeper.Core.Game;
using Minesweeper.Core.Persistence;
using Minesweeper.UI.Commands.Game;
using Minesweeper.UI.Commands.MainMenu;

namespace Minesweeper.UI.Input.InputStates;

public class GameOverInputState : InputState
{
    public GameOverInputState(Game game, IGameStateStore gameStateStore)
    {
        RegisterCommand(ConsoleKey.Enter, () => new StartNewGameCommand(game, gameStateStore));
        RegisterCommand(ConsoleKey.Z, () => new OpenMainMenuCommand());
    }
}