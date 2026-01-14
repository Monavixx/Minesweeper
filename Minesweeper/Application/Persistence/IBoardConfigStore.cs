using Minesweeper.Core.Generation;

namespace Minesweeper.Application.Persistence;

public interface IBoardConfigStore
{
    BoardConfig LoadOrCreateDefault();
}