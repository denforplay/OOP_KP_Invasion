using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;

namespace Invasion.Models.Players.Decorator
{
    /// <summary>
    /// Represents freezed player
    /// </summary>
    public class FreezedPlayer : PlayerDecorator
    {
        public override float Speed => 0;

        /// <summary>
        /// Freezed player decorator
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="components">Components</param>
        /// <param name="playerConfig">Player configuration</param>
        /// <param name="layer">Layer</param>
        public FreezedPlayer(Player player, List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(player, components, playerConfig, layer)
        {
        }
    }
}
