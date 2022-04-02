using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using System.Drawing;

namespace Invasion.Models.Factories.EnemiesFactories
{
    public class KamikadzeEnemyFactory : IModelFactory<KamikadzeEnemy>
    {
        private string _spriteFileName = "kamikadzeEnemy.png";
        private DX2D _dx2D;
        private CollisionController _collisionController;

        public KamikadzeEnemyFactory(DX2D dx2D, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _dx2D = dx2D;
        }

        public KamikadzeEnemy Create()
        {
            KamikadzeEnemy enemy = new KamikadzeEnemy(new List<IComponent>
            {
                new Transform(),
                new RigidBody2D()
            }, new EnemyConfiguration(2, 1, 1), Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(_dx2D, _spriteFileName));
            enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
            return enemy;
        }
    }
}
