using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons;

public abstract class WeaponBase : GameObject
{
    public WeaponConfiguration Configuration { get; init; }
    public abstract GameObject Parent { get; }
    public abstract void GiveDamage(IHealthable healthable);
    public virtual float ReloadTime { get; set; }
    public virtual float Speed { get; set; }
    public abstract void Attack(Vector2 direction);
    public abstract void Update();
    public virtual int Damage { get; set; }

    protected WeaponBase(WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(components, layer)
    {
        Configuration = configuration;
        Speed = configuration.Speed;
        ReloadTime = configuration.ReloadTime;
        Damage = configuration.Damage;
    }
}