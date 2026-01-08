using Minesweeper.Core.Generation;

namespace Minesweeper.Core;

public class AppState
{
    public BoardConfig BoardConfig { get; set; }

    public AppState()
    {
        BoardConfig = new BoardConfig()
        {
            MineChance = 5,
            Width = 20,
            Height = 20
        };
    }
}