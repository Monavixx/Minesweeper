using System.Runtime.InteropServices;
using Minesweeper.Application.Viewport;
using Minesweeper.Core.Game;

namespace Minesweeper.Application.Screens;

public class ScreenFactory
{
    private readonly Dictionary<Type, Func<IViewport, IScreen>> _creators = new();

    public void Register<TScreen>(Func<IViewport, IScreen> creator) where TScreen : IScreen
    {
        _creators.Add(typeof(TScreen), creator);
    }
    public IScreen Create(Type screenType, IViewport viewport)
    {
        if (_creators.TryGetValue(screenType, out var creator))
        {
            return creator(viewport);
        }
        throw new ArgumentException($"Cannot find creator for {screenType}");
        return null;
    }

    public IScreen Create<TScreen>(IViewport viewport) where TScreen : IScreen
    {
        return Create(typeof(TScreen), viewport);
    }
}