using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using SharpDX;

namespace Invasion.Models.Weapons.Firearms.Bullets;

public class BulletBase : GameObject
{
    private int _damage = 5;
    private GameObject _parent;
    public int Damage => _damage;

    public GameObject Parent => _parent;
    public BulletBase(List<IComponent> components, Layer layer = Layer.Bullet) : base(components, layer)
    {
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent;
        if (TryTakeComponent(out Transform transform) && _parent.TryTakeComponent(out Transform parentTransform))
        {
            transform.Position = parentTransform.Position;
        }
    }
}