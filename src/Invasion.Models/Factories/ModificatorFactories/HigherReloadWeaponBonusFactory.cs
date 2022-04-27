using System.Drawing;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Bonuses;

namespace Invasion.Models.Factories.ModificatorFactories;

public class HigherReloadWeaponBonusFactory : IModelFactory<HigherReloadWeaponBonus>
{
    private string _spriteFileName = @"Sources\lowerReload.png";
    private IGraphicProvider _graphicProvider;
    private CollisionController _collisionController;
    
    public HigherReloadWeaponBonusFactory(IGraphicProvider graphicProvider, CollisionController collisionController)
    {
        _collisionController = collisionController;
        _graphicProvider = graphicProvider;
    }
    
    public HigherReloadWeaponBonus Create()
    {
        var speedBonus = new HigherReloadWeaponBonus(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_graphicProvider, _spriteFileName),
        }, new Configurations.ModificatorConfiguration(1000));

        var boxCollider = new BoxCollider2D(_collisionController, speedBonus, new Size(2, 2));
        speedBonus.AddComponent(boxCollider);
        return speedBonus;
    }
}