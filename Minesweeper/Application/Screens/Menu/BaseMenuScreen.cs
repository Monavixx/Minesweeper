using System.Diagnostics.CodeAnalysis;
using Minesweeper.Application.Commands;
using Minesweeper.Application.Input;
using Minesweeper.Application.Input.InputStates;
using Minesweeper.Application.Render;
using Minesweeper.Application.Viewport;

namespace Minesweeper.Application.Screens.Menu;

public class BaseMenuScreen : Screen, IMenuContext
{
    private readonly BaseMenuRenderer _baseMenuRenderer;
    protected readonly List<MenuOption> Options = [];

    public int SelectedIndex
    {
        get;
        set => field = (value + Options.Count) % Options.Count; // wrap around
    }

    public List<string> OptionLabels => Options.Select(o => o.Label).ToList();

    public InputHandleResult? ExecuteSelected()
    {
        return Options[SelectedIndex].CommandCreator().Execute();
    }

    [SetsRequiredMembers]
    public BaseMenuScreen(IViewport viewport) : base(viewport)
    {
        _baseMenuRenderer = new BaseMenuRenderer(this);
        Init();
    }
    [SetsRequiredMembers]
    public BaseMenuScreen(IViewport viewport, BaseMenuRenderer baseMenuRenderer) : base(viewport)
    {
        _baseMenuRenderer = baseMenuRenderer;
        Init();
    }
    private void Init()
    {
        Viewport.OnChanged += HandleViewportChanged;
        SetInputState(new BaseMenuInputState(this));
    }

    private void HandleViewportChanged()
        => _baseMenuRenderer.RequestFullRender();
    

    protected void RegisterOption(string label, Func<ICommand> commandCreator)
    {
        Options.Add(new MenuOption(label, commandCreator));
    }
    public override void Render()
    {
        _baseMenuRenderer.Render();
    }

    protected record MenuOption(string Label, Func<ICommand> CommandCreator);

    public override void Dispose()
    {
        Viewport.OnChanged -= HandleViewportChanged;
        base.Dispose();
    }
}