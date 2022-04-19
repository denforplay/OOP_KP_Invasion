using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Weapons.Firearms.Bullets;
using System.Drawing;

namespace Invasion.Models.Factories.BulletFactories;

public class DefaultBulletFactory : IModelFactory<DefaultBullet>
{
    private string _spriteFileName = @"Sources\defaultBullet.png";
    private DirectXGraphicsProvider _dx2D;
    private CollisionController _collisionController;

    public DefaultBulletFactory(DirectXGraphicsProvider dx2D, CollisionController collisionController)
    {
        _dx2D = dx2D;
        _collisionController = collisionController;
    }
    
    public DefaultBullet Create()
    {
        var bullet = new DefaultBullet(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_dx2D, _spriteFileName)
        });
        
        var rigidBody = new RigidBody2D();
        bullet.AddComponent(new BoxCollider2D(_collisionController, bullet, new Size(1, 1)));
        bullet.AddComponent(rigidBody);
        return bullet;
    }
}