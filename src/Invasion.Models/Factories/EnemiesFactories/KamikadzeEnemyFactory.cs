using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using System.Drawing;

namespace Invasion.Models.Factories.EnemiesFactories
{
    public class KamikadzeEnemyFactory : IModelFactory<KamikadzeEnemy>
    {
        private string _spriteFileName = @"Sources\kamikadzeEnemy.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;

        public KamikadzeEnemyFactory(IGraphicProvider graphicProvider, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
        }

        public KamikadzeEnemy Create()
        {
            KamikadzeEnemy enemy = new KamikadzeEnemy(new List<IComponent>
            {
                new Transform(),
                new RigidBody2D()
            }, new EnemyConfiguration(health:2, speed:3f, cost:1), Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(_graphicProvider, _spriteFileName));
            enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
            return enemy;
        }
    }
}
