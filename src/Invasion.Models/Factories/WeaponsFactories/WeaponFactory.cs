using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
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
    private IGraphicProvider _graphicProvider;

    public WeaponFactory(CollisionController collisionController, BulletSystem bulletSystem, IGraphicProvider graphicProvider)
    {
        _collisionController = collisionController;
        _bulletSystem = bulletSystem;
        _graphicProvider = graphicProvider;
    }
    
    public WeaponBase Create(GameObject parent, Type type)
    {
        if (type == typeof(Knife))
        {
            Knife knife = new Knife(parent, new WeaponConfiguration(1f, 1f, 1), new List<IComponent>
            {
                new Transform(),
                new SpriteRenderer(_graphicProvider, _knifeSprite),
            });

            return knife;
        }
        else if (type == typeof(Pistol))
        {
            Pistol pistol = new Pistol(new DefaultBulletFactory(_graphicProvider, _collisionController), _bulletSystem, 
                new WeaponConfiguration(1f, 1f, 2),
                new List<IComponent>
            {
                new Transform(),
                new SpriteRenderer(_graphicProvider, _pistolSprite),
            }, parent);


            return pistol;
        }
        else
        {
            return new EmptyWeapon(new WeaponConfiguration(0, 0, 0), 
                new List<IComponent>()
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