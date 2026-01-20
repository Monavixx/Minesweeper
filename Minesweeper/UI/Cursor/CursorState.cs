namespace Minesweeper.UI.Cursor;

public class CursorState
{
    public int X { get; private set; } = 0;
    public int Y { get; private set; } = 0;
    public event Action<int, int>? OnMoved;
    public void Move(MoveDirection direction, int width, int height)
    {
        switch (direction)
        {
            case MoveDirection.Down:
                if (height <= ++Y)
                {
                    Y = 0;
                }
                break;
            case MoveDirection.Up:
                if (Y <= 0)
                {
                    Y = height - 1;
                }
                else
                {
                    --Y;
                }
                break;
            case MoveDirection.Right:
                if (width <= ++X)
                {
                    X = 0;
                }
                break;
            case MoveDirection.Left:
                if (X <= 0)
                {
                    X = width - 1;
                }
                else
                {
                    --X;
                }
                break;
        }
        OnMoved?.Invoke(X, Y);
    }
}