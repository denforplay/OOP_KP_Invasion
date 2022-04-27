using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;

namespace Invasion.Models.Players.Decorator
{
    public class FreezedPlayer : PlayerDecorator
    {
        public override float Speed => 0;
        public FreezedPlayer(Player player, List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(player, components, playerConfig, layer)
        {
        }
    }
}
