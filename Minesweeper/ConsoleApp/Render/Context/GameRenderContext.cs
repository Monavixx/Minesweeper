namespace Minesweeper.ConsoleApp.Render;

public class GameRenderContext : IRenderContext
{
    public void Render(IConsoleRenderer consoleRenderer)
    {
        consoleRenderer.RenderGame();
    }
}