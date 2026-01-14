using System.Text.Json.Serialization;

namespace Minesweeper.Core.Board;

public class Board
{
    [JsonPropertyName("Cells")]
    public required Cell[][] Cells { get; init; }
    public int Width => Cells.Length;
    public int Height => Cells[0].Length;
    
    public Cell this[int x, int y] => Cells[x][y];
    
    public required Cell[] MineCells { get; init; }

    public bool AllSafeCellsRevealed()
    {
        foreach (var columns in Cells)
        {
            foreach (var cell in columns)
            {
                if (cell is { IsMine: false, IsRevealed: false }) return false;
            }
        }

        return true;
    }
    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}