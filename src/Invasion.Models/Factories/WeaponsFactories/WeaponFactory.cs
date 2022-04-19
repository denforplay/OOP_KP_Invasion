using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Factories.BulletFactories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Melee;

namespace Invasion.Models.Factories.WeaponsFactories;

public class WeaponFactory
{
    private string _knifeSprite = @"Sources\knife.png";
    private string _pistolSprite = @"Sources\pistol.png";
    private CollisionController _collisionController;
    private BulletSystem _bulletSystem;
    private DirectXGraphicsProvider _dx2D;

    public WeaponFactory(CollisionController collisionController, BulletSystem bulletSystem, DirectXGraphicsProvider dx2D)
    {
        _collisionController = collisionController;
        _bulletSystem = bulletSystem;
        _dx2D = dx2D;
    }
    
    public WeaponBase Create(GameObject parent, Type type)
    {
        if (type == typeof(Knife))
        {
            Knife knife = new Knife(parent, new List<IComponent>
            {
                new Transform(),
                new SpriteRenderer(_dx2D, _knifeSprite),
            });

            return knife;
        }
        else if (type == typeof(Pistol))
        {
            Pistol pistol = new Pistol(new DefaultBulletFactory(_dx2D, _collisionController), _bulletSystem, new List<IComponent>
            {
                new Transform(),
                new SpriteRenderer(_dx2D, _pistolSprite),
            }, parent);


            return pistol;
        }
        else
        {
            return new EmptyWeapon(new List<IComponent>()
            {
                new Transform()
            });
        }
    }

    public WeaponBase Create<T>(GameObject parent) where T : WeaponBase
    {
        return Create(parent, typeof(T));
    }
}