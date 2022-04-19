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
    public class ShootingEnemyFactory : IModelFactory<ShootingEnemy>
    {
        private string _spriteFileName = @"Sources\shootingEnemy.png";
        private DirectXGraphicsProvider _dx2D;
        private CollisionController _collisionController;

        public ShootingEnemyFactory(DirectXGraphicsProvider dx2D, CollisionController collisionController)
        {
            _collisionController = collisionController;
            _dx2D = dx2D;
        }

        public ShootingEnemy Create()
        {
            ShootingEnemy enemy = new ShootingEnemy(new List<IComponent>
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