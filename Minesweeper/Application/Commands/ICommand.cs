using Minesweeper.Application.Input;
using Minesweeper.Application.Screens;

namespace Minesweeper.Application.Commands;

public interface ICommand
{
    InputHandleResult? Execute();
}