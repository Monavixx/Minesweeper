namespace Minesweeper.Core.Game;

using Board;

public class GameState
{
    public Board Board { get; private set; }
    public CurrentState State { get; set; }
    
    public GameState(Board board)
    {
        Board = board;
    }
    
    public enum CurrentState
    {
        Playing,
        GameOver,
        Victory
    }
}