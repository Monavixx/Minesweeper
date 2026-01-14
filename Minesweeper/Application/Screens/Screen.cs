using System.Diagnostics.CodeAnalysis;
using Minesweeper.Application.Input;
using Minesweeper.Application.Input.InputStates;
using Minesweeper.Application.Viewport;

namespace Minesweeper.Application.Screens;

public abstract class Screen : IScreen, IScreenContext
{
    private InputState? _inputState = null;

    public void SetInputState(InputState inputState)
    {
        _inputState = inputState;
    }

    public abstract void Render();
    public InputHandleResult? HandleInput(ConsoleKeyInfo keyInfo)
    {
        return _inputState?.HandleInput(keyInfo);
    }

    [SetsRequiredMembers]
    protected Screen(IViewport viewport)
    {
        Viewport = viewport;
    }

    public required IViewport Viewport { get; init; }
    public virtual void Update(TimeSpan deltaTime) { }

    public virtual void Dispose()
    {
    }
}