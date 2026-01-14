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
    private readonly Game _game;
    
    [SetsRequiredMembers]
    public GameScreen(IViewport viewport, Game game, IGameStateStore gameStateStore, Statistics statistics) : base(viewport)
    {
        _game = game;
        _gameRenderer = new GameRenderer(game, _cursor, statistics);
        _gameInputState = new GameInputState(game, _cursor, gameStateStore);
        _gameOverInputState = new GameOverInputState(game);

        // New game is created before GameScreen is, so OnGameStarted hasn't called HandleGameStarted,
        // so we have to handle it.
        if (game.GameState.State == GameState.CurrentState.Playing) HandleGameStarted();
        else HandleGameOver();
        
        game.OnGameOver += HandleGameOver;
        game.OnGameStarted += HandleGameStarted;
        game.OnVictory += HandleVictory;
    }

    private void HandleGameOver() => SetInputState(_gameOverInputState);
    private void HandleGameStarted() => SetInputState(_gameInputState);
    private void HandleVictory() => SetInputState(_gameOverInputState);

    public override void Render()
    {
        _gameRenderer.Render();
    }

    public override void Dispose()
    {
        _game.OnGameOver -= HandleGameOver;
        _game.OnGameStarted -= HandleGameStarted;
        _game.OnVictory -= HandleGameOver;
        _gameRenderer.Dispose();
        base.Dispose();
    }

    public override void Update(TimeSpan deltaTime)
    {
        base.Update(deltaTime);
        _game.Update(deltaTime);
    }
}