using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Factories;
using Invasion.Models.Interfaces;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Bullets;
using System.Numerics;

namespace Invasion.Models.Weapons.Firearms.Base;

/// <summary>
/// Represents base firearm
/// </summary>
public abstract class FirearmBase : WeaponBase
{
    public override void GiveDamage(IHealthable healthable)
    {
        healthable.TakeDamage(Damage);
    }

    private GameObject _parent;
    private BulletSystem _bulletSystem;
    private IModelFactory<BulletBase> _bulletFactory;
    public override float ReloadTime { get; set; }
    public override int Damage { get; set; }
    public override GameObject Parent => _parent;
    public override float Speed { get; set; }

    /// <summary>
    /// Firearm base constructor
    /// </summary>
    /// <param name="bulletFactory">Bullet factory</param>
    /// <param name="bulletSystem">Bullet system</param>
    /// <param name="configuration">Weapon configuration</param>
    /// <param name="components">Components</param>
    /// <param name="parent">Parent</param>
    public FirearmBase(IModelFactory<BulletBase> bulletFactory, BulletSystem bulletSystem, 
        WeaponConfiguration configuration, List<IComponent> components, GameObject parent) : base(configuration, components, Layer.Weapon)
    {
        _bulletSystem = bulletSystem; 
        _parent = parent;
        _bulletFactory = bulletFactory;
    }

    public override void Attack(Vector2 direction)
    {
        var bullet = GetBullet(direction);
        _bulletSystem.Work(bullet);
    }

    public override void Update()
    {
        if (TryTakeComponent(out Transform transform) && _parent.TryTakeComponent(out Transform parentTransform))
        {
            transform.Position = parentTransform.Position;
            transform.Rotation = parentTransform.Rotation;
        }
    }

    /// <summary>
    /// Get bullet method
    /// </summary>
    /// <param name="direction">Bullet direction</param>
    /// <returns>New entity of bullet</returns>
    protected virtual BulletBase GetBullet(Vector2 direction)
    {
        var bullet = _bulletFactory.Create();
        bullet.SetParent(this);
        if (bullet.TryTakeComponent(out RigidBody2D rigidBody2D))
        {
            rigidBody2D.Speed = direction;
        }
        
        return bullet;
    }
}