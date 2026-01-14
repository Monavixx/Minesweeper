using System.Diagnostics.CodeAnalysis;
using Minesweeper.Application.Commands;
using Minesweeper.Application.Commands.Game;
using Minesweeper.Application.Cursor;
using Minesweeper.Application.Input;
using Minesweeper.Application.Input.InputStates;
using Minesweeper.Application.Persistence;
using Minesweeper.Application.Render;
using Minesweeper.Application.Viewport;
using Minesweeper.Core.Game;
using Minesweeper.Core.Statistics;

namespace Minesweeper.Application.Screens;

public class GameScreen : Screen
{
    private readonly CursorState _cursor = new();
    private readonly GameRenderer _gameRenderer;
    private readonly GameInputState _gameInputState;
    private readonly GameOverInputState _gameOverInputState;
    
    [SetsRequiredMembers]
    public GameScreen(IViewport viewport, Game game, IGameStateStore gameStateStore, Statistics statistics) : base(viewport)
    {
        _gameRenderer = new GameRenderer(game, _cursor, statistics);
        _gameInputState = new GameInputState(game, _cursor, gameStateStore);
        _gameOverInputState = new GameOverInputState(game);
        SetInputState(_gameInputState);

        game.OnGameOver += HandleGameOver;
        game.OnGameStarted += HandleGameStarted;
        game.OnVictory += HandleGameOver; // Temporary solution
    }

    private void HandleGameOver() => SetInputState(_gameOverInputState);
    private void HandleGameStarted() => SetInputState(_gameInputState); 
    public override void Render()
    {
        _gameRenderer.Render();
    }

    public override void Dispose()
    {
        _gameRenderer.Dispose();
        base.Dispose();
    }
}