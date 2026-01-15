namespace Minesweeper.Core.Time;

public class GameTimer
{
    public TimeSpan Elapsed { get; set; }
    public bool IsRunning { get; private set; }

    public void Start() => IsRunning = true;
    public void Stop() => IsRunning = false;
    public void Reset() => Elapsed = TimeSpan.Zero;
    public void Update(TimeSpan deltaTime)
    {
        if(IsRunning)
            Elapsed += deltaTime;
    }
}