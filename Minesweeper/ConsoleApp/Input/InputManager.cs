namespace Minesweeper.ConsoleApp.Input;

public class InputManager (ConsoleAppState consoleAppState)
{
    public IInputContext? CurrentContext {get; set;}
    private readonly GlobalInputContext _globalInputContext = new GlobalInputContext(consoleAppState);

    public void HandleInput(ConsoleKeyInfo keyInfo)
    {
        var command = CurrentContext?.Resolve(keyInfo)
                    ?? _globalInputContext.Resolve(keyInfo);
        command?.Execute();
    }
}