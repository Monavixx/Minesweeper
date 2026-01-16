using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Minesweeper.Core.Board;

public class Board
{
    public Board(Cell[][] cells)
    {
        Cells = cells;
        for(int x = 0; x < Width; x++)
        for(int y = 0; y < Height; y++)
        {
            if (!Cells[x][y].IsMine) continue;
            foreach (var (x1, y1) in Cell.AroundCells)
            {
                if (IsValidPosition(x + x1, y + y1))
                {
                    ++cells[x + x1][y + y1].MinesAround;
                }
            }
        }
    }

    [JsonPropertyName("Cells")]
    public Cell[][] Cells { get; }

    public int Width => Cells.Length;
    public int Height => Cells[0].Length;
    
    public Cell this[int x, int y] => Cells[x][y];

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
        return IsValidPosition(x, y, Width, Height);
    }
    public static bool IsValidPosition(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}