using System.Text.Json.Serialization;

namespace Minesweeper.Core.Board;

public sealed class Cell : IEquatable<Cell>
{
    public bool IsMine { get; init; }
    [JsonInclude] public bool IsFlagged { get; private set; }
    [JsonInclude] public bool IsRevealed { get; private set; }
    [JsonInclude] public int MinesAround { get; set; }
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

    public bool Equals(Cell? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsFlagged == other.IsFlagged 
               && IsMine == other.IsMine 
               && IsRevealed == other.IsRevealed
               && MinesAround == other.MinesAround;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Cell other && Equals(other);
    }

    public override int GetHashCode() => IsMine.GetHashCode();

    public override string ToString()
    {
        return
            $"{nameof(IsMine)}: {IsMine}, {nameof(IsFlagged)}: {IsFlagged}, {nameof(IsRevealed)}: {IsRevealed}, {nameof(MinesAround)}: {MinesAround}, {nameof(IsEmpty)}: {IsEmpty}";
    }
}