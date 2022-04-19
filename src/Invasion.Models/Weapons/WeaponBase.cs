using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons;

public abstract class WeaponBase : GameObject
{
    public abstract GameObject Parent { get; }
    public abstract void GiveDamage(IHealthable healthable);
    public virtual float ReloadTime { get; }
    public abstract void Attack(Vector2 direction);
    public abstract void Update();

    protected WeaponBase(List<IComponent> components, Layer layer = Layer.Weapon) : base(components, layer)
    {
    }
}