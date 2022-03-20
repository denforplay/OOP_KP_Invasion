using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Weapons.Firearms.Bullets;
using SharpDX;

namespace Invasion.Models.Weapons.Firearms.Base;

public class FirearmBase : GameObject, IWeapon
{
    public event Action<BulletBase> OnShotEvent;
    private GameObject _parent;
    private DX2D _dx2D;

    public FirearmBase(DX2D dx2D, List<IComponent> components, GameObject parent, Layer layer = Layer.Default) : base(components, layer)
    {
        _dx2D = dx2D;
        _parent = parent;
    }


    public void Attack(Vector2 direction)
    {
        OnShotEvent?.Invoke(GetBullet(direction));
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
        
        bullet.SetParent(_parent);
        var rigidBody = new RigidBody2D();
        rigidBody.Speed = direction;
        bullet.AddComponent(rigidBody);
        return bullet;
    }
}