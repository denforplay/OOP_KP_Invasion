using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.View.Factories.Base;

namespace Invasion.View.Factories.BulletFactories;

public class DefaultBulletFactory : GameObjectViewFactoryBase<BulletBase>, IBulletFactory
{
    private string _defaultBulletSprite = "defaultBullet.png";
    
    public DefaultBulletFactory(DX2D dx2D) : base(dx2D)
    {
    }

    protected override GameObject GetEntity(BulletBase entity)
    {
        var bullet = Create();
        bullet.Components.AddRange(entity.Components);
        return bullet;
    }

    public BulletBase Create()
    {
        var bullet = new DefaultBullet();
        bullet.AddComponent(new SpriteRenderer(_dx2D, _defaultBulletSprite));
        return bullet;
    }
}