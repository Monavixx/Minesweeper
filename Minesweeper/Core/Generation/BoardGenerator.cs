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
        
        Cell[,] cells = new Cell[width, height];
        List<(int, int)> mines = new();
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                bool isMine = _random.Next(100) < mineChance;
                cells[x, y] = new Cell(isMine);
                if (isMine)
                {
                    mines.Add((x, y));
                }
            }
        }

        Cell[] mineCells = new Cell[mines.Count];
        int i = 0;
        foreach (var (x, y) in mines)
        {
            foreach (var (x1, y1) in Cell.AroundCells)
            {
                if (IsValidPosition(x + x1, y + y1))
                {
                    ++cells[x + x1, y + y1].MinesAround;
                }
            }
            mineCells[i++] = cells[x, y];
        }
        
        return new Board()
        {
            Cells = cells,
            MineCells = mineCells
        };

        bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
    }
    
    
}