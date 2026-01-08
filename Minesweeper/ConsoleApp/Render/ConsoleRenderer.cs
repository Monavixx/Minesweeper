using Minesweeper.Core.Board;
using Minesweeper.Core.Game;

namespace Minesweeper.ConsoleApp.Render;

public class ConsoleRenderer(ConsoleAppState consoleAppState) : IConsoleRenderer
{
    public void RenderGame()
    {
        Console.Clear();
        Game game = consoleAppState.Game;
        
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
                Console.SetCursorPosition(x+1, y+1);
                Cell cell = board[x, y];
                bool isCursor = x == consoleAppState.CursorState.X && y == consoleAppState.CursorState.Y;
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
        }

        if (game.GameState.State == GameState.CurrentState.Victory)
        {
            RenderVictory();
        }
        else if (game.GameState.State == GameState.CurrentState.GameOver)
        {
            RenderGameOver();
        }
        
        Console.SetCursorPosition(0, boardHeight+2);
    }

    public void RenderMainMenu()
    {
        throw new NotImplementedException();
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