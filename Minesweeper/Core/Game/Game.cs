using Minesweeper.Core.Generation;

namespace Minesweeper.Core.Game;
using Board;
using Statistics;

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
        if (cell.IsEmpty)
        {
            FloodReveal(x, y);
        }
        else if (cell.Reveal())
        {
            if (cell.IsMine)
            {
                GameState.State = GameState.CurrentState.GameOver;
                OnGameOver?.Invoke();
                return;
            }
            if (GameState.Board.AllSafeCellsRevealed())
            {
                GameState.State = GameState.CurrentState.Victory;
                OnVictory?.Invoke();
            }    
            OnCellUpdated?.Invoke(x, y);
        }
    }

    private bool RevealJustOne(int x, int y)
    {
        Cell cell = GameState.Board[x, y];
        if (cell.Reveal())
        {
            if (cell.IsMine)
            {
                GameState.State = GameState.CurrentState.GameOver;
                OnGameOver?.Invoke();
            }
            else if (GameState.Board.AllSafeCellsRevealed())
            {
                GameState.State = GameState.CurrentState.Victory;
                OnVictory?.Invoke();
            }
            OnCellUpdated?.Invoke(x, y);
            return true;
        }
        return false;
    }

    private void FloodReveal(int x, int y)
    {
        Queue<(int, int)> revealQueue = new ();
        HashSet<(int, int)> visited = new();
        revealQueue.Enqueue((x, y));
        visited.Add((x, y));
        
        while (revealQueue.Count > 0)
        {
            var (cx, cy) = revealQueue.Dequeue();
            Cell cell = GameState.Board[cx, cy];
            
            if (RevealJustOne(cx, cy) && cell.IsEmpty)
            {
                foreach (var aroundCell in Cell.AroundCells)
                {
                    int acx = aroundCell.Item1+cx;
                    int acy = aroundCell.Item2+cy;
                    if (GameState.Board.IsValidPosition(acx, acy)
                        && !visited.Contains((acx, acy)))
                    {
                        revealQueue.Enqueue((acx, acy));
                        visited.Add((acx, acy));
                    }
                }
            }
        }
    }
}