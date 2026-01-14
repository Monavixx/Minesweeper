using Minesweeper.Application.Viewport;

namespace Minesweeper.Application.Screens;

public interface IScreenContext
{
    IViewport Viewport { get; }
}