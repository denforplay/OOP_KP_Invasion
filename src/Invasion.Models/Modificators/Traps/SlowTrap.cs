using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Decorator;

namespace Invasion.Models.Modificators.Traps;

public class SlowTrap : TrapBase
{
    public SlowTrap(List<IComponent> components, Layer layer = Layer.Modificator) : base(components, layer)
    {
    }

    protected async override void Apply(Player player)
    {
        if (player is PlayerDecorator playerDecorator)
        {
            var previous = playerDecorator.Player;
            playerDecorator.SetPlayer(new SlowedPlayer(previous, previous.Components, new Configurations.PlayerConfiguration(1, 5)));
            await Task.Delay(3000);
            playerDecorator.SetPlayer(previous);
        }
    }
}
