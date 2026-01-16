namespace Minesweeper.Core.Generation;
using Board;

public class BoardGenerator : IBoardGenerator
{
    private Random _random;
    public BoardGenerator(Random? random = null)
    {
        _random = random ?? Random.Shared;
    }
    public Board Generate(BoardConfig boardConfig)
    {
        int width = boardConfig.Width;
        int height = boardConfig.Height;
        int mineChance = boardConfig.MineChance;
        
        Cell[][] cells = new Cell[width][];
        List<(int, int)> mines = new();
        for (int x = 0; x < width; ++x)
        {
            cells[x] =  new Cell[height];
            for (int y = 0; y < height; ++y)
            {
                bool isMine = _random.Next(100) < mineChance;
                cells[x][y] = new Cell(isMine);
                if (isMine)
                {
                    mines.Add((x, y));
                }
            }
        }

        

        return new Board(cells);
    }
}