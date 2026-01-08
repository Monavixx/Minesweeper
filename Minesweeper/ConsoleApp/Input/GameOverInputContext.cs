using Minesweeper.Core.Commands;

namespace Minesweeper.ConsoleApp.Input;

public class GameOverInputContext(ConsoleAppState consoleAppState) : IInputContext
{
    public ICommand? Resolve(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            return new StartNewGameCommand(consoleAppState.Game, consoleAppState.AppState.BoardConfig);
        }

        return null;
    }
}