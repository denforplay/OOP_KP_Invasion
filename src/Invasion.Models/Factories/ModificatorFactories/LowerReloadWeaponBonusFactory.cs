using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Bonuses;
using System.Drawing;

namespace Invasion.Models.Factories.ModificatorFactories
{
    /// <summary>
    /// Class to create instance of lower reload weapon bonus
    /// </summary>
    public class LowerReloadWeaponBonusFactory : IModelFactory<LowerReloadWeaponBonus>
    {
        private string _spriteFileName = @"Sources\higherReload.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;

        /// <summary>
        /// Lower weapon bonus factory constructor
        /// </summary>
        /// <param name="graphicProvider">Graphics provider</param>
        /// <param name="collisionController">Collision controller</param>
        public LowerReloadWeaponBonusFactory(IGraphicProvider graphicProvider, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
        }

        public LowerReloadWeaponBonus Create()
        {
            var slowedBonus = new LowerReloadWeaponBonus(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_graphicProvider, _spriteFileName),
        }, new Configurations.ModificatorConfiguration(1000));

            var boxCollider = new BoxCollider2D(_collisionController, slowedBonus, new Size(2, 2));
            slowedBonus.AddComponent(boxCollider);
            return slowedBonus;
        }
    }
}
