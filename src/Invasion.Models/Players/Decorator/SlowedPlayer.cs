using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Decorator;

/// <summary>
/// Class represents slowed player
/// </summary>
public class SlowedPlayer : PlayerDecorator
{
    public override float Speed { get => _player.Speed/2f; }

    /// <summary>
    /// Slowed player constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="components">Components</param>
    /// <param name="playerConfig">Player configuration</param>
    /// <param name="layer">Layer</param>
    public SlowedPlayer(Player player, List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(player, components, playerConfig, layer)
    {
    }
}