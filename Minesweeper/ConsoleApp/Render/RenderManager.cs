namespace Minesweeper.ConsoleApp.Render;

public class RenderManager(IConsoleRenderer consoleRenderer)
{
    public IRenderContext? RenderContext { get; set; }

    public void Render()
    {
        RenderContext?.Render(consoleRenderer);
    }
}