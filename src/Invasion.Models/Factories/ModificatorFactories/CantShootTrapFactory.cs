using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Traps;
using System.Drawing;

namespace Invasion.Models.Factories.ModificatorFactories
{
    public class CantShootTrapFactory : IModelFactory<CantShootTrap>
    {
        private string _spriteFileName = @"Sources\CantShootTrap.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;

        public CantShootTrapFactory(IGraphicProvider graphicProvider, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
        }

        public CantShootTrap Create()
        {
            var cantShootTrap = new CantShootTrap(new List<IComponent>
            {
            new Transform(),
            new SpriteRenderer(_graphicProvider, _spriteFileName),
            },
            new Configurations.ModificatorConfiguration(3000));

            var boxCollider = new BoxCollider2D(_collisionController, cantShootTrap, new Size(2, 2));
            cantShootTrap.AddComponent(boxCollider);
            return cantShootTrap;
        }
    }
}
