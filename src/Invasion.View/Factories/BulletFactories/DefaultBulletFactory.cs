using System.Drawing;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Collisions;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.View.Factories.Base;

namespace Invasion.View.Factories.BulletFactories;

public class DefaultBulletFactory : GameObjectViewFactoryBase<BulletBase>
{
    private string _defaultBulletSprite = "defaultBullet.png";
    private CollisionController _collisionController;
    
    public DefaultBulletFactory(DX2D dx2D, CollisionController collisionController) : base(dx2D)
    {
        _collisionController = collisionController;
    }
}