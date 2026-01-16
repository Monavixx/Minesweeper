using Minesweeper.Core.Board;
using Minesweeper.Core.Game;
using Minesweeper.Core.Time;
using Moq;

namespace UnitTests;

[TestFixture]
public class GameStateTests
{
    private Mock<ITimeProvider> _timeProviderMock = new();
    [Test]
    public void ResumePause_WhenResumeIsCalledTheFirstTimeAndThenPauseIsCalled_AccumulatedPlayTimeShouldBeZero()
    {
        _timeProviderMock.Setup(timeProvider => timeProvider.UtcNow).Returns(
            () => new DateTime(2026, 1, 16, 12, 0, 0));
        GameState gameState = new(new Board([]), 0, _timeProviderMock.Object);
        gameState.Resume();
        gameState.Pause();
        Assert.That(gameState.AccumulatedPlayTime, Is.EqualTo(TimeSpan.Zero));
    }
    [Test]
    public void ResumePause_WhenResumeIsCalledTwice_DoesNotResetTime()
    {
        _timeProviderMock.Setup(timeProvider => timeProvider.UtcNow).Returns(
            () => new DateTime(2026, 1, 16, 12, 0, 0));
        GameState gameState = new(new Board([]), 0, _timeProviderMock.Object);
        gameState.Resume();
        _timeProviderMock.Setup(timeProvider => timeProvider.UtcNow).Returns(
            () => new DateTime(2026, 1, 16, 13, 0, 0));
        gameState.Resume();
        gameState.Pause();
        Assert.That(gameState.AccumulatedPlayTime, Is.EqualTo(new TimeSpan(1,0,0)));
    }

    [Test]
    public void Pause_WithoutResume_DoesNothing()
    {
        _timeProviderMock.Setup(timeProvider => timeProvider.UtcNow).Returns(
            () => new DateTime(2026, 1, 16, 12, 0, 0));
        GameState gameState = new(new Board([]), 0, _timeProviderMock.Object);
        _timeProviderMock.Setup(timeProvider => timeProvider.UtcNow).Returns(
            () => new DateTime(2026, 1, 16, 13, 0, 0));
        gameState.Pause();
        Assert.That(gameState.AccumulatedPlayTime, Is.EqualTo(TimeSpan.Zero));
    }
}