using Minesweeper.Application.Input;
using Minesweeper.Application.Persistence;
using Minesweeper.Application.Screens;
using Minesweeper.Core.Persistence;

namespace Minesweeper.Application.Commands.MainMenu;

public class LoadGameCommand (Core.Game.Game game, IGameStateStore gameStateStore, int gameId) : ICommand
{
    public InputHandleResult? Execute()
    {
        game.LoadGame(gameStateStore.Load(gameId));
        return InputHandleResult.NavigateTo<GameScreen>();
    }
}