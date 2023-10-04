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

    /// <summary>
    /// Class to create instance of kamikadze enemy
    /// </summary>
    public class KamikadzeEnemyFactory : IModelFactory<KamikadzeEnemy>
    {
        private string _spriteFileName = @"Sources\kamikadzeEnemy.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;
        private EnemyConfiguration _configuration;

        /// <summary>
        /// Kamikadze enemy factory constructor
        /// </summary>
        /// <param name="graphicProvider">Graphic provider</param>
        /// <param name="collisionController">Collision controller</param>
        /// <param name="configuration">Enemy configuration</param>
        public KamikadzeEnemyFactory(IGraphicProvider graphicProvider, CollisionController collisionController, EnemyConfiguration configuration)
        {
            _configuration = configuration;
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
        }

        public KamikadzeEnemy Create()
        {
            KamikadzeEnemy enemy = new KamikadzeEnemy(new List<IComponent>
            {
                new Transform(),
                new RigidBody2D()
            }, _configuration);
            enemy.AddComponent(new SpriteRenderer(_graphicProvider, _spriteFileName));
            enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
            return enemy;
        }
    }
}
