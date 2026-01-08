namespace Minesweeper.Core.Board;

public sealed class Cell
{
    public bool IsFlagged { get; private set; }
    public bool IsMine { get; init; }
    public bool IsRevealed { get; private set; }
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
}