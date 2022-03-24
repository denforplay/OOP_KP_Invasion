using Invasion.Core.Interfaces;
using Invasion.Engine;
using SharpDX;

namespace Invasion.Models.Weapons;

public abstract class WeaponBase : GameObject
{
    public abstract void GiveDamage(IHealthable healthable);
    public virtual float ReloadTime { get; }
    public abstract void Attack(Vector2 direction);
    public abstract void Update();

    protected WeaponBase(List<IComponent> components, Layer layer = Layer.Weapon) : base(components, layer)
    {
    }
}