namespace Minesweeper.Application.Viewport;

public interface IViewport
{
    int Height { get; }
    int Width { get; }
    void Update();
    event Action? OnChanged;
    
}