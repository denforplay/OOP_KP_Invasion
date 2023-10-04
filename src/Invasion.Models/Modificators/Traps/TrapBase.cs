using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;

namespace Invasion.Models.Modificators.Traps;

/// <summary>
/// Class represents base trap
/// </summary>
public abstract class TrapBase : ModificatorBase
{
    /// <summary>
    /// Trap base constructor
    /// </summary>
    /// <param name="components">List of components</param>
    /// <param name="configuration">Configuration</param>
    /// <param name="layer">Layer</param>
    protected TrapBase(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, configuration, layer)
    {
    }

    public sealed override void Apply(GameObject gameObject)
    {
        if (gameObject is PlayerDecorator player && !IsApplied)
        {
            while (player.Player is PlayerDecorator decorator)
            {
                player.SetPlayer(decorator.Player);
            }

            IsApplied = true;
            Apply(player);
        }
    }

    /// <summary>
    /// Apply bonus on player
    /// </summary>
    /// <param name="player">Player</param>
    protected abstract void Apply(PlayerDecorator player);
}