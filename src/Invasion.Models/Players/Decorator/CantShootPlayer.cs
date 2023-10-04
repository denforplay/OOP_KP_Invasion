using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;

namespace Invasion.Models.Players.Decorator
{
    /// <summary>
    /// Cant shoot player
    /// </summary>
    public class CantShootPlayer : PlayerDecorator
    {
        public override bool CanDamage => false;

        /// <summary>
        /// Cant shoot player constructor
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="components">Components</param>
        /// <param name="playerConfig">Player configuration</param>
        /// <param name="layer">Layer</param>
        public CantShootPlayer(Player player, List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(player, components, playerConfig, layer)
        {
        }
    }
}
