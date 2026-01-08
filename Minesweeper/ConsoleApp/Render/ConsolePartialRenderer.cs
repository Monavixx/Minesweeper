using Minesweeper.Core.Board;
using Minesweeper.Core.Game;

namespace Minesweeper.ConsoleApp.Render;

public class ConsolePartialRenderer : IConsoleRenderer
{
    private bool _fullRenderRequired = true;
    private bool _gameOverRequired = false;
    private bool _victoryRequired = false;
    private (int,int) _oldCursorPosition = (0,0);
    private readonly List<(int, int)> _updatedCells = new();
    private ConsoleAppState _consoleAppState;
    public ConsolePartialRenderer(ConsoleAppState consoleAppState)
    {
        _consoleAppState = consoleAppState;
        _consoleAppState.Game.OnGameStarted += HandleGameStarted;
        _consoleAppState.Game.OnGameOver += HandleGameOver;
        _consoleAppState.Game.OnVictory += HandleVictory;
        _consoleAppState.Game.OnCellUpdated += HandleCellUpdated;
        _consoleAppState.OnCursorMoved += HandleCursorMoved;
    }
    private void ClearCache()
    {
        _fullRenderRequired = false;
        _updatedCells.Clear();
        _victoryRequired = false;
        _gameOverRequired = false;
    }
    private void HandleCellUpdated(int x, int y) => _updatedCells.Add((x, y));
    private void HandleGameStarted() => _fullRenderRequired = true;
    private void HandleGameOver() => _gameOverRequired = true;
    private void HandleVictory() => _victoryRequired = true;

    private void HandleCursorMoved(int x, int y)
    {
        _updatedCells.Add((x, y));
        _updatedCells.Add(_oldCursorPosition);
    }
    public void RenderGame()
    {
        _oldCursorPosition = (_consoleAppState.CursorState.X, _consoleAppState.CursorState.Y);
        if (_fullRenderRequired)
        {
            FullRender();
        }
        else
        {
            foreach (var updatedCell in _updatedCells)
            {
                RenderCell(updatedCell.Item1, updatedCell.Item2);
            }

            if (_gameOverRequired)
            {
                RenderGameOver();
            }
            if(_victoryRequired)
                RenderVictory();
        }
        ClearConsoleCursorPosition();
        ClearCache();
    }

    public void RenderMainMenu()
    {
        if (_fullRenderRequired)
        {
            const string mainMenuMessage = "Press enter to start";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            int width = mainMenuMessage.Length + 8;
            for (int i = 1; i < 4; ++i)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(new string(' ', width));
            }

            Console.SetCursorPosition(1 + (width - mainMenuMessage.Length) / 2, 2);
            Console.Write(mainMenuMessage);
            Console.ResetColor();
        }

        Console.SetCursorPosition(0, 4);
        ClearCache();
    }

    private void ClearConsoleCursorPosition()
    {
        Console.SetCursorPosition(0, _consoleAppState.Game.GameState.Board.Height+2);
    }

    private void RenderCell(int x, int y)
    {
        Console.ResetColor();
        Console.SetCursorPosition(x+1, y+1);
        Cell cell = _consoleAppState.Game.GameState.Board[x, y];
        bool isCursor = x == _consoleAppState.CursorState.X && y == _consoleAppState.CursorState.Y;
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

    private void FullRender()
    {
        Console.Clear();
        Game game = _consoleAppState.Game;
        
        int boardWidth = game.GameState.Board.Width;
        int boardHeight = game.GameState.Board.Height;
        Board board = game.GameState.Board;
        
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

        if (game.GameState.State == GameState.CurrentState.Victory)
        {
            RenderVictory();
        }
        else if (game.GameState.State == GameState.CurrentState.GameOver)
        {
            RenderGameOver();
        }
        
        ClearConsoleCursorPosition();
    }

    private void RenderVictory(string victoryMessage = "You won!")
    {
        var oldBack = Console.BackgroundColor;
        var oldFore = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        
        int width = victoryMessage.Length + 8;
        for(int i = 1; i < 4; ++i)
        {
            Console.SetCursorPosition(1, i);
            Console.Write(new string(' ', width));
        }
        Console.SetCursorPosition(1+(width-victoryMessage.Length)/2, 2);
        Console.Write(victoryMessage);
        
        Console.ForegroundColor = oldFore;
        Console.BackgroundColor = oldBack;
    }
    private void RenderGameOver(string gameOverMessage = "Game over :(")
    {
        var oldBack = Console.BackgroundColor;
        var oldFore = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        
        int width = gameOverMessage.Length + 8;
        for(int i = 1; i < 4; ++i)
        {
            Console.SetCursorPosition(1, i);
            Console.Write(new string(' ', width));
        }
        Console.SetCursorPosition(1+(width-gameOverMessage.Length)/2, 2);
        Console.Write(gameOverMessage);
        
        Console.ForegroundColor = oldFore;
        Console.BackgroundColor = oldBack;
    }
}