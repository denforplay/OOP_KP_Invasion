using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models.Modificators.Traps;

public abstract class TrapBase : ModificatorBase
{
    protected TrapBase(List<IComponent> components, Layer layer = Layer.Modificator) : base(components, layer)
    {
    }

    public sealed override void Apply(GameObject gameObject)
    {
        if (gameObject is Player player && !IsApplied)
        {
            IsApplied = true;
            Apply(player);
        }
    }

    protected abstract void Apply(Player player);
}