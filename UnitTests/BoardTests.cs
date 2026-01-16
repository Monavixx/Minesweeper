using Minesweeper.Core.Board;

namespace UnitTests;

[TestFixture]
public class BoardTests
{
    [Test]
    [TestCase(0, 0, 2)]
    [TestCase(1, 2, 4)]
    public void Constructor_WhenMinesProvided_SetsMinesAroundCorrectly(int x, int y, int minesAround)
    {
        Cell[][] cells =
        [
            [new Cell(false), new Cell(false), new Cell(true)],
            [new Cell(true), new Cell(true), new Cell(false)],
            [new Cell(false), new Cell(true), new Cell(true)],
            [new Cell(true), new Cell(true), new Cell(true)],
        ];
        Board board = new Board(cells);
        Assert.That(board[x, y].MinesAround, Is.EqualTo(minesAround));
    }
    [Test]
    public void AllSafeCellsRevealed_WhenAllCellsRevealed_ReturnsTrue()
    {
        Cell[][] cells =
        [
            [new Cell(false), new Cell(false), new Cell(true)],
            [new Cell(true), new Cell(true), new Cell(false)],
            [new Cell(false), new Cell(true), new Cell(true)],
            [new Cell(true), new Cell(true), new Cell(true)],
        ];
        foreach (var cell in cells.SelectMany(c=>c))
        {
            if (!cell.IsMine) cell.Reveal();
        }
        Board board = new Board(cells);
        Assert.That(board.AllSafeCellsRevealed, Is.True);
    }
    [Test]
    public void AllSafeCellsRevealed_WhenSomeCellsRevealed_ReturnsFalse()
    {
        Cell[][] cells =
        [
            [new Cell(false), new Cell(false), new Cell(true)],
            [new Cell(true), new Cell(false), new Cell(false)],
            [new Cell(true), new Cell(true), new Cell(true)],
            [new Cell(true), new Cell(false), new Cell(true)],
            [new Cell(true), new Cell(false), new Cell(false)],
        ];
        int i = 0;
        foreach (var cell in cells.SelectMany(c=>c))
        {
            if (!cell.IsMine && (i++)%2==0) cell.Reveal();
        }
        Board board = new Board(cells);
        Assert.That(board.AllSafeCellsRevealed, Is.False);
    }
    [Test]
    public void AllSafeCellsRevealed_WhenNoCellsRevealed_ReturnsFalse()
    {
        Cell[][] cells =
        [
            [new Cell(false), new Cell(true), new Cell(true), new Cell(false)],
            [new Cell(true), new Cell(true), new Cell(false), new Cell(true)],
            [new Cell(false), new Cell(false), new Cell(true), new Cell(false)],
            [new Cell(false), new Cell(true), new Cell(true), new Cell(true)]
        ];
        Board board = new Board(cells);
        Assert.That(board.AllSafeCellsRevealed, Is.False);
    }
    [Test]
    [TestCase(0, 0, 1, 1)]
    [TestCase(5, 12, 14, 13)]
    [TestCase(7, 0, 8, 8)]
    public void IsValidPosition_WhenValidPosition_ReturnsTrue(int x, int y, int width, int height)
    {
        Assert.That(Board.IsValidPosition(x, y, width, height), Is.True);
    }
    [Test]
    [TestCase(0, 1, 1, 1)]
    [TestCase(14, 12, 14, 13)]
    [TestCase(7, 80, 8, 8)]
    public void IsValidPosition_WhenInvalidPosition_ReturnsFalse(int x, int y, int width, int height)
    {
        Assert.That(Board.IsValidPosition(x, y, width, height), Is.False);
    }
}