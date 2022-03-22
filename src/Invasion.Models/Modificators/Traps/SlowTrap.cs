using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Modificators.Traps;

public class SlowTrap : TrapBase
{
    public SlowTrap(List<IComponent> components, Layer layer = Layer.Default) : base(components, layer)
    {
    }

    protected override void Apply(Player player)
    {
        
    }
}
