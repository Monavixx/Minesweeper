using Minesweeper.Core.Board;
using Minesweeper.Core.Game;
using Minesweeper.Core.Statistics;
using Minesweeper.UI.Cursor;

namespace Minesweeper.UI.Render;

public class GameRenderer : IDisposable
{
    private bool _fullRenderRequired = true;
    private (int,int) _oldCursorPosition = (0,0);
    private readonly List<(int, int)> _updatedCells = [];
    private readonly Game _game;
    private readonly CursorState _cursor;
    private readonly StatisticsRenderer _statisticsRenderer;
    private bool _stateRenderRequired = true;
    private int _oldSeconds = 0;
    private bool _timerRequired = true;

    private const int StateMinWidth = 15;
    private void ClearCache()
    {
        _fullRenderRequired = false;
        _updatedCells.Clear();
        _stateRenderRequired = false;
        _timerRequired = false;
    }

    public GameRenderer(Game game, CursorState cursor, Statistics statistics)
    {
        _statisticsRenderer = new StatisticsRenderer(statistics);
        _cursor = cursor;
        _game = game;
        game.OnCellUpdated += HandleCellUpdated;
        game.OnGameStarted += HandleGameStarted;
        game.OnVictory += HandleGameStateChanged;
        game.OnGameOver += HandleGameStateChanged;
        cursor.OnMoved += HandleCursorMoved;
    }

    private void HandleCellUpdated(int x, int y) => _updatedCells.Add((x, y));
    private void HandleGameStarted() => _fullRenderRequired = true;
    private void HandleGameStateChanged() => _stateRenderRequired = true;
    private void HandleCursorMoved(int x, int y)
    {
        _updatedCells.Add((x, y));
        _updatedCells.Add(_oldCursorPosition);
    }
    public void Dispose()
    {
        _game.OnCellUpdated -= HandleCellUpdated;
        _cursor.OnMoved -= HandleCursorMoved;
        _game.OnGameStarted -= HandleGameStarted;
    }
    public void Render()
    {
        var board = _game.GameState.Board;
        _oldCursorPosition = (_cursor.X, _cursor.Y);
        if (_fullRenderRequired)
        {
            FullRender();
            _statisticsRenderer.FullRender(board.Width + 3, 7);
            RenderState(board.Width + 3, 3);
        }
        else
        {
            foreach (var updatedCell in _updatedCells)
            {
                RenderCell(updatedCell.Item1, updatedCell.Item2);
            }
            _statisticsRenderer.PartialRender(board.Width + 3, 7);
            if(_stateRenderRequired)
                RenderState(board.Width + 3, 3);
        }
        RenderTimer(board.Width + 3, 1);
        ClearCache();
    }

    private void FullRender()
    {
        Console.Clear();
        var board = _game.GameState.Board;
        int boardWidth = board.Width;
        int boardHeight = board.Height;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        
        Console.SetCursorPosition(1, 0);
        Console.Write(new string('=', boardWidth));
        Console.SetCursorPosition(1, boardHeight+1);
        Console.Write(new string('=', boardWidth));

        for (int i = 0; i < boardHeight + 2; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write('#');
            Console.SetCursorPosition(boardWidth+1, i);
            Console.Write('#');
        }
        Console.ResetColor();

        for (int x = 0; x < boardWidth; ++x)
        {
            for (int y = 0; y < boardHeight; ++y)
            {
                RenderCell(x, y);
            }
        }
        
        Console.ResetColor();
    }
    
    private void RenderCell(int x, int y)
    {
        var board = _game.GameState.Board;
        Console.ResetColor();
        Console.SetCursorPosition(x+1, y+1);
        Cell cell = board[x, y];
        bool isCursor = x == _cursor.X && y == _cursor.Y;
        if (cell.IsRevealed)
        {
            if (isCursor) Console.BackgroundColor = ConsoleColor.DarkYellow;
            if (cell.IsMine)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('*');
            }
            else if (!cell.IsEmpty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(cell.MinesAround);
            }
            else
            {
                Console.Write(' ');
            }
        }
        else if (cell.IsFlagged)
        {
            Console.BackgroundColor = isCursor ? ConsoleColor.DarkYellow : ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('F');
        }
        else
        {
            Console.BackgroundColor = isCursor ? ConsoleColor.Yellow : ConsoleColor.DarkGreen;
            Console.Write(' ');
        }
        Console.ResetColor();
    }

    private void RenderState(int x, int y)
    {
        string message="";
        int width=0;
        if (_game.GameState.State == GameState.CurrentState.GameOver)
        {
            message = "Game Over";
            width = int.Max(StateMinWidth, message.Length);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (_game.GameState.State == GameState.CurrentState.Victory)
        {
            message = "Victory";
            width = int.Max(StateMinWidth, message.Length);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
        }
        for (int i = 0; i < 3; ++i)
        {
            Console.SetCursorPosition(x, y+i);
            Console.Write(new string(' ', width));
        }
        Console.SetCursorPosition(x+(width-message.Length)/2, y+1);
        Console.Write(message);
        Console.ResetColor();
    }

    private void RenderTimer(int x, int y)
    {
        var seconds = _game.Timer.Elapsed.Seconds;
        if (seconds != _oldSeconds || _timerRequired)
        {
            var timerString = 
                $"{_game.Timer.Elapsed.TotalHours:00}:{_game.Timer.Elapsed.Minutes:00}:{seconds:00}";
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(timerString);
            Console.ResetColor();
        }

        _oldSeconds = seconds;
    }
}