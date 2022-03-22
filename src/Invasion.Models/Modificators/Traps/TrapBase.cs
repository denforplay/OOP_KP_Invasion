using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Modificators.Traps;

public abstract class TrapBase : ModificatorBase
{
    protected TrapBase(List<IComponent> components, Layer layer = Layer.Default) : base(components, layer)
    {
    }

    public sealed override void Apply(GameObject gameObject)
    {
        if (gameObject is Player player)
        {
            Apply(player);
            IsApplied = true;
        }
    }

    protected abstract void Apply(Player player);
}