using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Factories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Bullets;
using SharpDX;

namespace Invasion.Models.Weapons.Firearms.Base;

public class FirearmBase : GameObject, IWeapon
{
    private float _reloadTime;
    
    public void GiveDamage(IHealthable healthable)
    {
        healthable.TakeDamage(_damage);
    }

    public float ReloadTime => _reloadTime;
    private GameObject _parent;
    private BulletSystem _bulletSystem;
    private int _damage;
    private IModelFactory<BulletBase> _bulletFactory;
    public GameObject Parent => _parent;
    public FirearmBase(IModelFactory<BulletBase> bulletFactory, BulletSystem bulletSystem, List<IComponent> components, GameObject parent) : base(components, Layer.Weapon)
    {
        _damage = 1;
        _reloadTime = 1f;
        _bulletSystem = bulletSystem; 
        _parent = parent;
        _bulletFactory = bulletFactory;
    }

    public void Attack(Vector2 direction)
    {
        var bullet = GetBullet(direction);
        _bulletSystem.Work(bullet);
    }

    public void Update()
    {
        if (TryTakeComponent(out Transform transform) && _parent.TryTakeComponent(out Transform parentTransform))
        {
            transform.Position = parentTransform.Position;
            transform.Rotation = parentTransform.Rotation;
        }
    }

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