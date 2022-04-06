using System.Drawing;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Traps;

namespace Invasion.Models.Factories.ModificatorFactories;

public class SlowTrapFactory : IModelFactory<SlowTrap>
{
    private string _spriteFileName = @"Sources\slowTrap.png";
    private DX2D _dx2D;
    private CollisionController _collisionController;
    
    public SlowTrapFactory(DX2D dx2D, CollisionController collisionController)
    {
        _collisionController = collisionController;
        _dx2D = dx2D;
    }
    
    public SlowTrap Create()
    {
        var slowTrap = new SlowTrap(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_dx2D, _spriteFileName),
        }, 
        new Configurations.ModificatorConfiguration(1000));

        var boxCollider = new BoxCollider2D(_collisionController, slowTrap, new Size(2, 2));
        slowTrap.AddComponent(boxCollider);
        return slowTrap;
    }
}