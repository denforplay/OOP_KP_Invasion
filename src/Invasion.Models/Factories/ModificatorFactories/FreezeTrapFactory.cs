using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Traps;
using System.Drawing;

namespace Invasion.Models.Factories.ModificatorFactories
{
    /// <summary>
    /// Class to create instance of freeze trap
    /// </summary>
    public class FreezeTrapFactory : IModelFactory<FreezeTrap>
    {
        private string _spriteFileName = @"Sources\freezeTrap.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;

        /// <summary>
        /// Freeze trap factory
        /// </summary>
        /// <param name="graphicProvider">Graphic provider</param>
        /// <param name="collisionController">Collision controller</param>
        public FreezeTrapFactory(IGraphicProvider graphicProvider, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
        }

        public FreezeTrap Create()
        {
            var freezeTrap = new FreezeTrap(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_graphicProvider, _spriteFileName),
        },
            new Configurations.ModificatorConfiguration(500));

            var boxCollider = new BoxCollider2D(_collisionController, freezeTrap, new Size(2, 2));
            freezeTrap.AddComponent(boxCollider);
            return freezeTrap;
        }
    }
}
