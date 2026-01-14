using Minesweeper.Core.Game;

namespace Minesweeper.Application.Persistence;

public interface IGameStateStore
{
    void Save(GameState state);
    GameState Load(int id);
    int[] FindAllGameIds();
}