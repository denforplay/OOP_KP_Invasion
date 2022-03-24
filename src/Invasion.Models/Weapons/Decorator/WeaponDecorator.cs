﻿using Invasion.Core.Interfaces;
using Invasion.Engine;
using SharpDX;

namespace Invasion.Models.Weapons.Decorator;

public class WeaponBaseDecorator : WeaponBase
{
    public void SetWeapon(WeaponBase weaponBase)
    {
        DecoratedWeaponBase = weaponBase;
    }

    public override float ReloadTime { get => DecoratedWeaponBase.ReloadTime; }
    protected WeaponBase DecoratedWeaponBase;
    public WeaponBase Weapon => DecoratedWeaponBase;
    public override void GiveDamage(IHealthable healthable)
    {
        DecoratedWeaponBase.GiveDamage(healthable);
    }

    public override void Attack(Vector2 direction)
    {
        DecoratedWeaponBase.Attack(direction);
    }

    public override void Update()
    {
        DecoratedWeaponBase.Update();
    }

    public WeaponBaseDecorator(WeaponBase weaponBase, List<IComponent> components, Layer layer = Layer.Weapon) : base(components, layer)
    {
        DecoratedWeaponBase = weaponBase;
    }
}