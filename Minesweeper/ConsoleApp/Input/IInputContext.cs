using Minesweeper.Core.Commands;

namespace Minesweeper.ConsoleApp.Input;

public interface IInputContext
{
    ICommand? Resolve(ConsoleKeyInfo keyInfo);
}