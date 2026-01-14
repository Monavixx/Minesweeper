using Minesweeper.Application.Input;
using Minesweeper.Application.Viewport;

namespace Minesweeper.Application.Screens;

public interface IScreen : IDisposable
{
    void Render();
    InputHandleResult? HandleInput(ConsoleKeyInfo key);
    IViewport Viewport { get; }
    void Update(TimeSpan deltaTime);
}