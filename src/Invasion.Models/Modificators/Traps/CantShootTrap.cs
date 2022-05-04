using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;
using Invasion.Models.Players.Decorator;

namespace Invasion.Models.Modificators.Traps
{
    public class CantShootTrap : TrapBase
    {
        public CantShootTrap(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, configuration, layer)
        {
        }

        protected async override void Apply(PlayerDecorator player)
        {
            var previous = player.Player;
            player.SetPlayer(new CantShootPlayer(previous, previous.Components, new PlayerConfiguration(1, 5)));
            await Task.Delay(_configuration.Duration);
            player.SetPlayer(previous);
        }
    }
}
