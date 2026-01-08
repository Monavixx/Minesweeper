namespace Minesweeper.Core.Generation;
using Board;

public interface IBoardGenerator
{
    Board Generate(BoardConfig boardConfig);
}