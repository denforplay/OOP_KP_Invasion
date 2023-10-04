using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;

namespace Invasion.Models.Modificators.Bonuses;

/// <summary>
/// Abstract class represents base bonus object
/// </summary>
public abstract class BonusBase : ModificatorBase
{
    /// <summary>
    /// Bonus base constructor
    /// </summary>
    /// <param name="components">Components</param>
    /// <param name="configuration">Configurations</param>
    /// <param name="layer">Layer</param>
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

    /// <summary>
    /// Method to apply bonus on weapon
    /// </summary>
    /// <param name="weaponBase">Weapon</param>
    protected abstract void Apply(WeaponBaseDecorator weaponBase);
}