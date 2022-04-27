using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators.Bonuses;
using System.Drawing;

namespace Invasion.Models.Factories.ModificatorFactories
{
    public class LowerReloadWeaponBonusFactory : IModelFactory<LowerReloadWeaponBonus>
    {
        private string _spriteFileName = @"Sources\higherReload.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;

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
