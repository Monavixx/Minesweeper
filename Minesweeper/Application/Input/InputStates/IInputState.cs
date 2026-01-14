namespace Minesweeper.Application.Input.InputStates;

public interface IInputState
{
    InputHandleResult? HandleInput(ConsoleKeyInfo keyInfo);
}