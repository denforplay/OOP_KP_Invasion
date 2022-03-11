using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;

namespace Invasion.Models.Weapons.Firearms.Bullets;

public class BulletBase : GameObject
{
    private int _damage = 5;
    private Transform _parent;
    public int Damage => _damage;
    
    public BulletBase(List<IComponent> components, Layer layer = Layer.Bullet) : base(components, layer)
    {
    }

    public void SetParent(Transform parent)
    {
        if (TryTakeComponent(out Transform transform))
        {
            transform.Position = parent.Position;
        }
    }
}