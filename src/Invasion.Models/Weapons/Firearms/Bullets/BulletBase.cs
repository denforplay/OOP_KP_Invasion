using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Weapons.Firearms.Base;

namespace Invasion.Models.Weapons.Firearms.Bullets;

/// <summary>
/// Represents base bullet
/// </summary>
public class BulletBase : GameObject
{
    private FirearmBase _parent;
    public bool IsUsed { get; set; }

    /// <summary>
    /// Bullet parent
    /// </summary>
    public FirearmBase Parent => _parent;
    public BulletBase(List<IComponent> components, Layer layer = Layer.Bullet) : base(components, layer)
    {
    }

    public void SetParent(FirearmBase parent)
    {
        _parent = parent;
        if (TryTakeComponent(out Transform transform) && _parent.TryTakeComponent(out Transform parentTransform))
        {
            transform.Position = parentTransform.Position;
        }
    }
}