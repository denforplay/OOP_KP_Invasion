using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }, new EnemyConfiguration(2, 1, 1), Layer.Enemy);
            enemy.AddComponent(new SpriteRenderer(_dx2D, _spriteFileName));
            enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
            return enemy;
        }
    }
}
