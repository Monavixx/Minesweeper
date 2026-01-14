using Minesweeper.Application.Input;
using Minesweeper.Application.Persistence;
using Minesweeper.Application.Screens;

namespace Minesweeper.Application.Commands.MainMenu;

public class LoadGameCommand (Core.Game.Game game, IGameStateStore gameStateStore, int gameId) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.GameState = gameStateStore.Load(gameId);
        return InputHandleResult.NavigateTo<GameScreen>();
    }
}