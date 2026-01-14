using System.Collections.Immutable;
using Minesweeper.Application.Screens;
using Minesweeper.Application.Screens.Menu;

namespace Minesweeper.Application.Render;

public class BaseMenuRenderer (IMenuContext menuContext): IDisposable
{
    private const int LeftPadding = 1;
    private const int RightPadding = 1;
    private const int TopPadding = 0;
    private const int BottomPadding = 0;
    
    private int _scrollOffsetLines = 0;
    private int _lastSelectedIndex = 0;
    public int MinimumButtonWidth { get; set; }
    private bool _fullRenderRequired = true;
    public void RequestFullRender() => _fullRenderRequired = true;
    
    private int InnerHeight => menuContext.Viewport.Height - TopPadding - BottomPadding;
    private int InnerWidth => menuContext.Viewport.Width - LeftPadding - RightPadding;

    private int? _buttonWidth = null;
    private int ButtonWidth
    {
        get
        {
            _buttonWidth ??= int.Min(InnerWidth, menuContext.OptionLabels.MaxBy(s => s.Length)?.Length ?? 0);
            return _buttonWidth.Value;
        }
    }

    public virtual void Dispose() { }
    
    public virtual void Render()
    {
        Console.ResetColor();
        
        int startLine = GetStartLine(menuContext.SelectedIndex);
        int endLine = startLine + GetRenderedHeight(menuContext.OptionLabels[menuContext.SelectedIndex].Length);
        if (startLine < _scrollOffsetLines)
        {
            _scrollOffsetLines = startLine;
            _fullRenderRequired = true;
        }
        else if(endLine > _scrollOffsetLines + InnerHeight)
        {
            _scrollOffsetLines = endLine - InnerHeight;
            _fullRenderRequired = true;
        } //TODO: Неправильно рассчитывается _scrollOffsetLines
            // seven
            // EIGHT
            // nine
            // -> Extend console
            // -> ArrowUp
        
        //if (_fullRenderRequired)
            FullRender();
        /*else
        {
            RenderOption(options[selectedIndex], startLine, true, buttonWidth);
            RenderOption(options[_lastSelectedIndex], GetStartLine(options, _lastSelectedIndex), false,
                buttonWidth);
        }*/
        
        _lastSelectedIndex = menuContext.SelectedIndex;
        Console.ResetColor();
    }

    private int GetStartLine(int index)
    {
        int startLine = 0;
        for (int i = 0; i < index; ++i)
        {
            startLine += GetRenderedHeight(menuContext.OptionLabels[i].Length);
        }

        return startLine;
    }

    
    private void FullRender()
    {
        Console.Clear();
        int height = GetHeight();
        int renderedHeight = int.Min(height, InnerHeight);

        int row = TopPadding;
        List<(string line, bool isSelected)> allLines = AllLines.ToList();
        for (int i = _scrollOffsetLines; i < _scrollOffsetLines + renderedHeight; ++i)
        {
            RenderLine(allLines[i].line, row++, allLines[i].isSelected);
        }
    }

    
    /*private int RenderOption(int optionIndex, int row, bool isSelected)
    {
        foreach (var line in SplitOption(option))
        {
            if (_scrollOffsetLines <= row && row < InnerHeight + _scrollOffsetLines)
                RenderLine(line, row++, isSelected);
            else break;
        }

        return row;
    }*/

    private IEnumerable<(string, bool)> AllLines => menuContext.OptionLabels.SelectMany((_, index) => SplitOption(index));

    private IEnumerable<(string, bool)> SplitOption(int optionIndex)
    {
        int i = 0;
        while (menuContext.OptionLabels[optionIndex].Length > i*InnerWidth)
        {
            yield return (menuContext.OptionLabels[optionIndex].Substring((i++)*InnerWidth, 
                    int.Min(InnerWidth, menuContext.OptionLabels[optionIndex].Length)),
                optionIndex==menuContext.SelectedIndex);
        }
    }

    private void RenderLine(string line, int row, bool isSelected)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(LeftPadding, TopPadding + row);
        Console.Write(new string(' ', ButtonWidth));
        Console.SetCursorPosition(LeftPadding, TopPadding + row);
        if(isSelected) Console.BackgroundColor = ConsoleColor.Yellow;
        Console.Write(line);
        Console.BackgroundColor = ConsoleColor.DarkGray;
    }

    private int GetRenderedHeight(int length)
    {
        return int.Max(1, (length + InnerWidth - 1) / InnerWidth);
    }
    private int GetHeight()
        => menuContext.OptionLabels.Sum(option => GetRenderedHeight(option.Length));
}
