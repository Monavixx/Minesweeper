namespace Minesweeper.Application.Viewport;

public class ConsoleViewport : IViewport
{
    public int Height { get; private set; }
    public int Width { get; private set; }
    public event Action? OnChanged;
    public ConsoleViewport()
    {
        Height = Console.WindowHeight;
        Width = Console.WindowWidth;
    }
    public void Update()
    {
        int newHeight = Console.WindowHeight;
        int newWidth = Console.WindowWidth;
        if (newHeight != Height || newWidth != Width)
        {
            Height = newHeight;
            Width = newWidth;
            OnChanged?.Invoke();
        }
    }

}