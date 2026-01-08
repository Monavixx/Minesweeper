namespace Minesweeper.Core.Board;

public class Board
{
    public required Cell[,] Cells { get; init; }
    public int Width => Cells.GetLength(0);
    public int Height => Cells.GetLength(1);
    
    public Cell this[int x, int y] => Cells[x, y];
    
    public required Cell[] MineCells { get; init; }

    public bool AllSafeCellsRevealed()
    {
        foreach (var cell in Cells)
        {
            if (cell is { IsMine: false, IsRevealed: false }) return false;
        }

        return true;
    }
    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}