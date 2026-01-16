using Minesweeper.Core.Board;

namespace UnitTests;

[TestFixture]
public class CellTests
{
    [Test]
    public void ToggleFlagged_Test()
    {
        Cell cell = new Cell(true);
        cell.ToggleFlagged();
        Assert.That(cell.IsFlagged, Is.True);
        cell.ToggleFlagged();
        Assert.That(cell.IsFlagged, Is.False);
    }
    [Test]
    public void ToggleFlagged_And_Revealed_Test()
    {
        Cell cell = new Cell(true);
        cell.Reveal();
        Assert.That(cell.IsRevealed, Is.True);
        cell.ToggleFlagged();
        Assert.That(cell.IsFlagged, Is.False);
    }
}