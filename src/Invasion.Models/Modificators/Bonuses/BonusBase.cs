using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Weapons;

namespace Invasion.Models.Modificators.Bonuses;

public abstract class BonusBase : ModificatorBase
{
    protected BonusBase(List<IComponent> components, Layer layer = Layer.Modificator) : base(components, layer)
    {
    }
    
    public sealed override void Apply(GameObject gameObject)
    {
        if (gameObject is IWeapon weapon)
        {
            Apply(weapon);
            IsApplied = true;
        }
    }

    protected abstract void Apply(IWeapon weapon);
}