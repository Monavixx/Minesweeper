using ConsoleGameFramework.Input.InputStates;
using Minesweeper.Core.Game;
using Minesweeper.Core.Persistence;
using Minesweeper.UI.Commands.Game;
using Minesweeper.UI.Commands.MainMenu;
using Minesweeper.UI.Cursor;

namespace Minesweeper.UI.Input.InputStates;

public class GameInputState : InputState
{
    public GameInputState(Game game, CursorState cursor, IGameStateStore gameStateStore)
    {
        RegisterCommand(ConsoleKey.DownArrow, 
            () => new MoveCursorCommand(game.GameState.Board, cursor, MoveDirection.Down));
        RegisterCommand(ConsoleKey.UpArrow, 
            () => new MoveCursorCommand(game.GameState.Board, cursor, MoveDirection.Up));
        RegisterCommand(ConsoleKey.RightArrow, 
            () => new MoveCursorCommand(game.GameState.Board, cursor, MoveDirection.Right));
        RegisterCommand(ConsoleKey.LeftArrow, 
            () => new MoveCursorCommand(game.GameState.Board, cursor, MoveDirection.Left));
        RegisterCommand(ConsoleKey.F, () => new ToggleFlaggedCommand(game, cursor));
        RegisterCommand(ConsoleKey.Enter, ()=> new RevealCommand(game, cursor));
        RegisterCommand(ConsoleKey.S, ()=> new SaveGameCommand(gameStateStore, game.GameState));
        RegisterCommand(ConsoleKey.Z, () => new OpenMainMenuCommand());
    }
}