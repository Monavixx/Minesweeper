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
}