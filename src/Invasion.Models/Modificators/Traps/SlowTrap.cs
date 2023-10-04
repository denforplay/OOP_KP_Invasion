using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;

namespace Invasion.Models.Modificators.Traps;

/// <summary>
/// Class represents slow trap
/// </summary>
public class SlowTrap : TrapBase
{
    /// <summary>
    /// Slow trap constructor
    /// </summary>
    /// <param name="components">Components</param>
    /// <param name="configuration">Configuration</param>
    /// <param name="layer">Layer</param>
    public SlowTrap(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, configuration, layer)
    {
    }

    protected async override void Apply(PlayerDecorator player)
    {
        var previous = player.Player;
        player.SetPlayer(new SlowedPlayer(previous, previous.Components, new PlayerConfiguration(1, 5)));
        await Task.Delay(_configuration.Duration);
        player.SetPlayer(previous);
    }
}
