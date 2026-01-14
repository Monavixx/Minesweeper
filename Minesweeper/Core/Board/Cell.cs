using System.Text.Json.Serialization;

namespace Minesweeper.Core.Board;

public sealed class Cell
{
    [JsonInclude]
    public bool IsFlagged { get; private set; }
    public bool IsMine { get; init; }
    [JsonInclude]
    public bool IsRevealed { get; private set; }
    [JsonInclude]
    public int MinesAround { get; internal set; }
    public bool IsEmpty => MinesAround == 0;

    public Cell(bool isMine)
    {
        IsMine = isMine;
    }

    public void ToggleFlagged()
    {
        if (IsRevealed) return;
        IsFlagged = !IsFlagged;
    }
    
    public bool Reveal()
    {
        if(IsFlagged || IsRevealed)  return false;
        IsRevealed = true;
        return true;
    }
    
    public static readonly (int, int)[] AroundCells = [
        (-1,-1), (0, -1),(1, -1),
        (-1, 0),         (1, 0),
        (-1, 1), (0, 1), (1, 1)
    ];
}