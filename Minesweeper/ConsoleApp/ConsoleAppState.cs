using System.Runtime.CompilerServices;
using Minesweeper.ConsoleApp.Commands;
using Minesweeper.ConsoleApp.Input;
using Minesweeper.Core;
using Minesweeper.Core.Game;
using Minesweeper.Core.Statistics;

namespace Minesweeper.ConsoleApp;

public class ConsoleAppState(AppState appState, Game game, Statistics statistics)
{
    public AppState AppState { get; } = appState;
    public Game Game { get; } = game;
    public CursorState CursorState { get; } = new(){X=0, Y=0};
    public bool IsRunning { get; set; } = true;
    public Statistics Statistics { get; init; } = statistics;
    
    public event Action<int, int>? OnCursorMoved;

    public void BindStatisticsToEvents()
    {
        game.OnGameStarted += () => ++Statistics.GamesPlayed;
        game.OnGameOver += () => ++Statistics.GameOversPlayed;
        game.OnVictory += () => ++Statistics.VictoriesPlayed;
    }

    public void MoveCursor(MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.Down:
                if (Game.GameState.Board.Height <= ++CursorState.Y)
                {
                    CursorState.Y = 0;
                }
                break;
            case MoveDirection.Up:
                if (CursorState.Y <= 0)
                {
                    CursorState.Y = Game.GameState.Board.Height - 1;
                }
                else
                {
                    --CursorState.Y;
                }
                break;
            case MoveDirection.Right:
                if (Game.GameState.Board.Width <= ++CursorState.X)
                {
                    CursorState.X = 0;
                }
                break;
            case MoveDirection.Left:
                if (CursorState.X <= 0)
                {
                    CursorState.X = Game.GameState.Board.Width - 1;
                }
                else
                {
                    --CursorState.X;
                }
                break;
        }
        OnCursorMoved?.Invoke(CursorState.X, CursorState.Y);
    }
}
public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}