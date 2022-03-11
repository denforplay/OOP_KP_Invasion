using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Weapons.Firearms.Bullets;
using SharpDX;

namespace Invasion.Models.Weapons.Firearms.Base;

public class FirearmBase : GameObject, IWeapon
{
    public event Action<Vector2> OnAttackEvent;
    public event Action<BulletBase> OnShotEvent;
    private Transform _parent;

    public FirearmBase() : base()
    {
        
    }

    public FirearmBase(List<IComponent> components, Transform parent, Layer layer = Layer.Default) : base(components, layer)
    {
        _parent = parent;
    }


    public void Attack(Vector2 direction)
    {
        OnShotEvent?.Invoke(GetBullet(direction));
    }

    public void Update()
    {
        if (TryTakeComponent(out Transform transform))
        {
            transform.Position = _parent.Position;
            transform.Rotation = _parent.Rotation;
        }
    }

    protected virtual BulletBase GetBullet(Vector2 direction)
    {
        var bullet = new DefaultBullet(new List<IComponent>
        {
            new Transform()
        });
        bullet.SetParent(_parent);
        var rigidBody = new RigidBody2D();
        rigidBody.Speed = direction;
        bullet.AddComponent(rigidBody);
        return bullet;
    }
}