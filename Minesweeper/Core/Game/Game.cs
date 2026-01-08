using Minesweeper.Core.Generation;

namespace Minesweeper.Core.Game;
using Board;

public class Game
{
    public IBoardGenerator BoardGenerator { get; set; }

    public GameState GameState { get; private set; }

    public event Action? OnGameOver;
    public event Action? OnGameStarted;
    public event Action? OnVictory;
    public event Action<int, int>? OnCellUpdated;
    

    public Game(IBoardGenerator boardGenerator)
    {
        BoardGenerator = boardGenerator;
    }

    public void StartNewGame(BoardConfig boardConfig)
    {
        GameState = new GameState(BoardGenerator.Generate(boardConfig));
        OnGameStarted?.Invoke();
    }

    public void ToggleFlagged(int x, int y)
    {
        GameState.Board[x, y].ToggleFlagged();
        OnCellUpdated?.Invoke(x, y);
    }

    public void Reveal(int x, int y)
    {
        Cell cell = GameState.Board[x, y];
        if (cell.Reveal())
        {
            if (cell.IsMine)
            {
                GameState.State = GameState.CurrentState.GameOver;
                OnGameOver?.Invoke();
                return;
            }
            if (cell.IsEmpty)
            {
                FloodReveal(x, y);
            }

            if (GameState.Board.AllSafeCellsRevealed())
            {
                GameState.State = GameState.CurrentState.Victory;
                OnVictory?.Invoke();
            }            
        }
        OnCellUpdated?.Invoke(x, y);
    }

    private void FloodReveal(int x, int y)
    {
        
    }
}