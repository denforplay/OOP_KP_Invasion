using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Weapons;

namespace Invasion.Models.Modificators.Bonuses;

public abstract class BonusBase : ModificatorBase
{
    protected BonusBase(List<IComponent> components, Layer layer = Layer.Modificator) : base(components, layer)
    {
    }
    
    public sealed override void Apply(GameObject gameObject)
    {
        if (gameObject is WeaponBase weapon && !IsApplied)
        {
            IsApplied = true;
            Apply(weapon);
        }
    }

    protected abstract void Apply(WeaponBase weaponBase);
}