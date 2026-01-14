using System.Text.Json;
using Minesweeper.Core.Generation;

namespace Minesweeper.Application.Persistence;

public class JsonBoardConfigStore (AppPaths appPaths) : IBoardConfigStore
{
    public BoardConfig LoadOrCreateDefault()
    {
        if (!File.Exists(appPaths.BoardConfigFile))
            return DefaultConfig;
        return JsonSerializer.Deserialize<BoardConfig>(File.ReadAllText(appPaths.BoardConfigFile)) 
               ?? DefaultConfig;
    }

    private BoardConfig DefaultConfig => new BoardConfig()
    {
        Height = 10,
        Width = 20,
        MineChance = 10
    };
}