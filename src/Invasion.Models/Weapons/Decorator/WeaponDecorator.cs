using Invasion.Core.Interfaces;
using SharpDX;

namespace Invasion.Models.Weapons.Decorator;

public class WeaponDecorator : IWeapon
{
    public WeaponDecorator(IWeapon decoratedWeapon)
    {
        _decoratedWeapon = decoratedWeapon;
    }

    public void SetWeapon(IWeapon weapon)
    {
        _decoratedWeapon = weapon;
    }

    public virtual float ReloadTime
    {
        get => _decoratedWeapon.ReloadTime;
    }

    protected IWeapon _decoratedWeapon;
    
    public void GiveDamage(IHealthable healthable)
    {
        _decoratedWeapon.GiveDamage(healthable);
    }

    public void Attack(Vector2 direction)
    {
        _decoratedWeapon.Attack(direction);
    }

    public void Update()
    {
        _decoratedWeapon.Update();
    }
}