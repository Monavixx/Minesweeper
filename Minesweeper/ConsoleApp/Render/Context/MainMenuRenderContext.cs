namespace Minesweeper.ConsoleApp.Render;

public class MainMenuRenderContext : IRenderContext
{
    public void Render(IConsoleRenderer consoleRenderer)
    {
        consoleRenderer.RenderMainMenu();
    }
}