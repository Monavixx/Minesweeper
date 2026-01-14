using Minesweeper.Application.Input;

namespace Minesweeper.Application.Screens.Menu;

public interface IMenuContext : IScreenContext
{
    int SelectedIndex { get; set; }
    List<string> OptionLabels { get; }
    InputHandleResult? ExecuteSelected();
}