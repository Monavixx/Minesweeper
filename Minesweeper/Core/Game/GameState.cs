namespace Minesweeper.Core.Game;

using Board;

public class GameState(Board board)
{
    public Board Board { get; private set; } = board;
    public CurrentState State { get; set; }

    public enum CurrentState
    {
        Playing,
        GameOver,
        Victory
    }
}