using Minesweeper.Application.Commands.Game;
using Minesweeper.Application.Cursor;
using Minesweeper.Application.Persistence;
using Minesweeper.Core.Game;

namespace Minesweeper.Application.Input.InputStates;

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
    }
}