using System.Drawing;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Collisions;
using Invasion.Models.Factories.BulletFactories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Melee;

namespace Invasion.Models.Factories.WeaponsFactories;

public class WeaponFactory
{
    private string _knifeSprite = "knife.png";
    private string _pistolSprite = "pistol.png";
    private CollisionController _collisionController;
    private BulletSystem _bulletSystem;
    private DX2D _dx2D;

    public WeaponFactory(CollisionController collisionController, BulletSystem bulletSystem, DX2D dx2D)
    {
        _collisionController = collisionController;
        _bulletSystem = bulletSystem;
        _dx2D = dx2D;
    }
    
    public IWeapon Create<T>(GameObject parent) where T : IWeapon
    {
        if (typeof(T) == typeof(Knife))
        {
            Knife knife = new Knife(parent, new List<IComponent>
            {
                new Transform(),
                new SpriteRenderer(_dx2D, _knifeSprite),
            });

            var collider = new BoxCollider2D(_collisionController, knife, new Size(2, 1));
            knife.AddComponent(collider);
            return knife;
        }
        else if (typeof(T) == typeof(Pistol))
        {
            Pistol pistol = new Pistol(new DefaultBulletFactory(_dx2D, _collisionController), _bulletSystem, new List<IComponent>
            {
                new Transform(),
                new SpriteRenderer(_dx2D, _pistolSprite),
            }, parent);

            var collider = new BoxCollider2D(_collisionController, pistol, new Size(2, 2));
            pistol.AddComponent(collider);
            return pistol;
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}