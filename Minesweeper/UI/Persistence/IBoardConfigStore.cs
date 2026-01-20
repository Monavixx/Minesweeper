using Minesweeper.Core.Generation;

namespace Minesweeper.UI.Persistence;

public interface IBoardConfigStore
{
    BoardConfig LoadOrCreateDefault();
}