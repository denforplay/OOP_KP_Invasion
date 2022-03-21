using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Collisions;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Bullets;
using SharpDX;

namespace Invasion.Models.Weapons.Firearms.Base;

public class FirearmBase : GameObject, IWeapon
{
    public void GiveDamage(IHealthable healthable)
    {
        healthable.TakeDamage(_damage);
    }

    public float ReloadTime { get; set; }
    private GameObject _parent;
    private DX2D _dx2D;
    private BulletSystem _bulletSystem;
    private CollisionController _collisionController;
    private int _damage;
    public GameObject Parent => _parent;
    public FirearmBase(CollisionController collisionController, DX2D dx2D, BulletSystem bulletSystem, List<IComponent> components, GameObject parent, Layer layer = Layer.Default) : base(components, layer)
    {
        _damage = 1;
        ReloadTime = 1f;
        _collisionController = collisionController;
        _bulletSystem = bulletSystem; 
        _dx2D = dx2D;
        _parent = parent;
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
        var bullet = new DefaultBullet(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_dx2D, "defaultBullet.png")
        });
        
        bullet.SetParent(this);
        var rigidBody = new RigidBody2D();
        bullet.AddComponent(new BoxCollider2D(_collisionController, bullet, new System.Drawing.Size(1, 1)));
        rigidBody.Speed = direction;
        bullet.AddComponent(rigidBody);
        return bullet;
    }
}