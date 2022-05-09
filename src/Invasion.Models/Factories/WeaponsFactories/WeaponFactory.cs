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

/// <summary>
/// Represents class to create instances of weapon
/// </summary>
public class WeaponFactory
{
    private string _knifeSprite = @"Sources\knife.png";
    private string _pistolSprite = @"Sources\pistol.png";
    private CollisionController _collisionController;
    private BulletSystem _bulletSystem;
    private IGraphicProvider _graphicProvider;

    /// <summary>
    /// Weapon factory constructor
    /// </summary>
    /// <param name="collisionController">Collision controller</param>
    /// <param name="bulletSystem">Bullet system</param>
    /// <param name="graphicProvider">Graphic provider</param>
    public WeaponFactory(CollisionController collisionController, BulletSystem bulletSystem, IGraphicProvider graphicProvider)
    {
        _collisionController = collisionController;
        _bulletSystem = bulletSystem;
        _graphicProvider = graphicProvider;
    }
    
    /// <summary>
    /// Create weapon from weapon type
    /// </summary>
    /// <param name="parent">Parent of weapon</param>
    /// <param name="type">Type of weapon</param>
    /// <returns>Returns instance of weapon</returns>
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

    /// <summary>
    /// Create instance of weapon
    /// </summary>
    /// <typeparam name="T">Weapon type</typeparam>
    /// <param name="parent">Weapon parent</param>
    /// <returns>New instance of weapon</returns>
    public WeaponBase Create<T>(GameObject parent) where T : WeaponBase
    {
        return Create(parent, typeof(T));
    }
}