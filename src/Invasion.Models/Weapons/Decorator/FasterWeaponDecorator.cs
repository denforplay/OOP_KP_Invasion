namespace Invasion.Models.Weapons.Decorator;

public class FasterWeaponDecorator : WeaponDecorator
{
    public override float ReloadTime { get => base.ReloadTime/100f; }

    public FasterWeaponDecorator(IWeapon decoratedWeapon) : base(decoratedWeapon)
    {
    }
}