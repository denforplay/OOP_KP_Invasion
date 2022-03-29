using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Configurations;

namespace Invasion.Models.Decorator;

public class SlowedPlayer : PlayerDecorator
{
    public override float Speed { get => _player.Speed/2f; }

    public SlowedPlayer(Player player, List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(player, components, playerConfig, layer)
    {
    }
}