using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using System.Drawing;

namespace Invasion.Models.Factories.EnemiesFactories
{
    public class BeatingEnemyFactory : IModelFactory<BeatingEnemy>
    {
        private string _spriteFileName = @"Sources\beatingEnemy.png";
        private DX2D _dx2D;
        private CollisionController _collisionController;

        public BeatingEnemyFactory(DX2D dx2D, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _dx2D = dx2D;
        }

        public BeatingEnemy Create()
        {
            BeatingEnemy enemy = new BeatingEnemy(new List<IComponent>
            {
                new Transform(),
                new RigidBody2D()
            }, new EnemyConfiguration(health:2, speed:2f, cost:1), Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(_dx2D, _spriteFileName));
            enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
            return enemy;
        }
    }
}
