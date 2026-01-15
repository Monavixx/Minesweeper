using Minesweeper.Core.Game;

namespace Minesweeper.Core.Persistence;

public interface IGameStateStore
{
    void Save(GameState state);
    GameState Load(int id);
    int[] FindAllGameIds();
    int NewGameId();
}