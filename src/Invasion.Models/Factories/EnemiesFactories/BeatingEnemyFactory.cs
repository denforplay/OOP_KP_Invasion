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
    /// Class to create instance of beating enemy
    /// </summary>
    public class BeatingEnemyFactory : IModelFactory<BeatingEnemy>
    {
        private string _spriteFileName = @"Sources\beatingEnemy.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;
        private EnemyConfiguration _configuration;

        /// <summary>
        /// Beating enemy factory constructor
        /// </summary>
        /// <param name="graphicProvider">Graphics provider</param>
        /// <param name="collisionController">Collision controller</param>
        /// <param name="enemyConfiguration">Enemy configuration</param>
        public BeatingEnemyFactory(IGraphicProvider graphicProvider, CollisionController collisionController, EnemyConfiguration enemyConfiguration)
        {
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
            _configuration = enemyConfiguration;
        }

        public BeatingEnemy Create()
        {
            BeatingEnemy enemy = new BeatingEnemy(new List<IComponent>
            {
                new Transform(),
                new RigidBody2D()
            }, _configuration, Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(_graphicProvider, _spriteFileName));
            enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
            return enemy;
        }
    }
}
