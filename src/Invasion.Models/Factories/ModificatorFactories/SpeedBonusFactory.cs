using System.Drawing;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Bonuses;

namespace Invasion.Models.Factories.ModificatorFactories;

public class SpeedBonusFactory : IModelFactory<SpeedBonus>
{
    private string _spriteFileName = @"Sources\speedBonus.png";
    private DX2D _dx2D;
    private CollisionController _collisionController;
    
    public SpeedBonusFactory(DX2D dx2D, CollisionController collisionController)
    {
        _collisionController = collisionController;
        _dx2D = dx2D;
    }
    
    public SpeedBonus Create()
    {
        var speedBonus = new SpeedBonus(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_dx2D, _spriteFileName),
        });

        var boxCollider = new BoxCollider2D(_collisionController, speedBonus, new Size(2, 2));
        speedBonus.AddComponent(boxCollider);
        return speedBonus;
    }
}