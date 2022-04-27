﻿using Invasion.Engine;
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
    public class ShootingEnemyFactory : IModelFactory<ShootingEnemy>
    {
        private string _spriteFileName = @"Sources\shootingEnemy.png";
        private IGraphicProvider _graphicProvider;
        private CollisionController _collisionController;
        private EnemyConfiguration _enemyConfiguration;

        public ShootingEnemyFactory(IGraphicProvider graphicProvider, CollisionController collisionController, EnemyConfiguration enemyConfiguration)
        {
            _enemyConfiguration = enemyConfiguration;
            _collisionController = collisionController;
            _graphicProvider = graphicProvider;
        }

        public ShootingEnemy Create()
        {
            ShootingEnemy enemy = new ShootingEnemy(new List<IComponent>
            {
                new Transform(),
                new RigidBody2D()
            }, _enemyConfiguration);
            enemy.AddComponent(new SpriteRenderer(_graphicProvider, _spriteFileName));
            enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
            return enemy;
        }
    }
}