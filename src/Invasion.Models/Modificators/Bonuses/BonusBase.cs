using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;

namespace Invasion.Models.Modificators.Bonuses;

public abstract class BonusBase : ModificatorBase
{
    protected BonusBase(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, configuration, layer)
    {
    }

    public sealed override void Apply(GameObject gameObject)
    {
        if (gameObject is WeaponBaseDecorator weapon && !IsApplied)
        {
            while (weapon.Weapon is WeaponBaseDecorator decorator)
            {
                weapon.SetWeapon(decorator.Weapon);
            }

            IsApplied = true;
            Apply(weapon);
        }
    }

    protected abstract void Apply(WeaponBaseDecorator weaponBase);
}