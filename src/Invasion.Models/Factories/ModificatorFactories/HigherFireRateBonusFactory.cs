using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Bonuses;
using System.Drawing;

namespace Invasion.Models.Factories.ModificatorFactories
{
    public class HigherFireRateBonusFactory : IModelFactory<HigherFIreRateWeaponBonus>
    {
        private string _spriteFileName = @"Sources\speedWeaponBonus.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;

        public HigherFireRateBonusFactory(IGraphicProvider graphicProvider, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
        }

        public HigherFIreRateWeaponBonus Create()
        {
            var speedBonus = new HigherFIreRateWeaponBonus(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_graphicProvider, _spriteFileName),
        }, new Configurations.ModificatorConfiguration(1000));

            var boxCollider = new BoxCollider2D(_collisionController, speedBonus, new Size(2, 2));
            speedBonus.AddComponent(boxCollider);
            return speedBonus;
        }
    }
}
