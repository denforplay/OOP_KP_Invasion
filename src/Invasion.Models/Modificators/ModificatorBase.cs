using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Modificators;

public abstract class ModificatorBase : GameObject, IAppliable
{
    public ModificatorBase(List<IComponent> components, Layer layer = Layer.Default) : base(components, layer)
    {
    }

    public bool IsApplied { get; set; }
    public abstract void Apply(GameObject gameObject);
}