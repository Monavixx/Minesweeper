using System.Text.Json;
using Minesweeper.Core.Game;
using Minesweeper.Core.Persistence;

namespace Minesweeper.Application.Persistence;

public class JsonGameStateStore : IGameStateStore
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly AppPaths _appPaths;
    public JsonGameStateStore(AppPaths appPaths)
    {
        _appPaths = appPaths;
        _jsonSerializerOptions = new JsonSerializerOptions();
        _jsonSerializerOptions.Converters.Add(new BoardJsonConverter());
    }
    public void Save(GameState state)
    {
        string json = JsonSerializer.Serialize(state, _jsonSerializerOptions);
        Directory.CreateDirectory(_appPaths.GamesDirectory);
        File.WriteAllText(
            Path.Combine(_appPaths.GamesDirectory, state.GameId+"."+_appPaths.GameFileExtension), json);
    }

    public GameState Load(int id)
    {
        string json = File.ReadAllText(Path.Combine(_appPaths.GamesDirectory, id+"."+_appPaths.GameFileExtension));
        return JsonSerializer.Deserialize<GameState>(json, _jsonSerializerOptions)!;
    }

    public int[] FindAllGameIds()
    {
        DirectoryInfo di = new DirectoryInfo(_appPaths.GamesDirectory);
        if (!di.Exists) return [];
        return di.GetFiles($"*.{_appPaths.GameFileExtension}")
            .Select(info => int.Parse(info.Name[..info.Name.LastIndexOf('.')]))
            .ToArray();
    }

    public int NewGameId()
    {
        var gameIds = FindAllGameIds();
        if (gameIds.Length == 0) return 0;
        return gameIds.Max() + 1;
    }
}