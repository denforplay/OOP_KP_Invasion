using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons;

/// <summary>
/// Class represents weapon base
/// </summary>
public abstract class WeaponBase : GameObject
{
    /// <summary>
    /// Weapon configuration
    /// </summary>
    public WeaponConfiguration Configuration { get; init; }

    /// <summary>
    /// Weapon parent
    /// </summary>
    public abstract GameObject Parent { get; }

    /// <summary>
    /// Give damage to some healthable object
    /// </summary>
    /// <param name="healthable">Object to be given damage</param>
    public abstract void GiveDamage(IHealthable healthable);

    /// <summary>
    /// Weapon reload time
    /// </summary>
    public virtual float ReloadTime { get; set; }

    /// <summary>
    /// Weapon speed
    /// </summary>
    public virtual float Speed { get; set; }

    /// <summary>
    /// Weapon attack method
    /// </summary>
    /// <param name="direction">Attack direction</param>
    public abstract void Attack(Vector2 direction);

    /// <summary>
    /// Update weapon statement
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// Weapon damage
    /// </summary>
    public virtual int Damage { get; set; }

    /// <summary>
    /// Weapon base constructor
    /// </summary>
    /// <param name="configuration">Weapon configuration</param>
    /// <param name="components">Components</param>
    /// <param name="layer">Layer</param>
    protected WeaponBase(WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(components, layer)
    {
        Configuration = configuration;
        Speed = configuration.Speed;
        ReloadTime = configuration.ReloadTime;
        Damage = configuration.Damage;
    }
}