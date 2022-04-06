using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;

namespace Invasion.Models.Modificators.Traps;

public abstract class TrapBase : ModificatorBase
{
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

    protected abstract void Apply(PlayerDecorator player);
}