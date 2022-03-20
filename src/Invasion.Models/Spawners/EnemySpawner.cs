using System.Drawing;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Collisions;
using Invasion.Models.Enemies;
using Invasion.Models.Systems;
using SharpDX;

namespace Invasion.Models.Spawners;

public class EnemySpawner
{
    private EnemySystem _enemySystem;
    private readonly Func<EnemyBase>[] _variants;
    private readonly Random _random = new Random();
    private readonly CollisionController _collisionController;
    private Vector3[] _spawnPositions;
    private DX2D _dx2D;
    public EnemySpawner(EnemySystem enemySystem, CollisionController controller, DX2D dx2D)
    {
        _dx2D = dx2D;
        _collisionController = controller;
        _enemySystem = enemySystem;
        _variants = new Func<EnemyBase>[]
        {
            CreateShootingEnemy
        };
        _spawnPositions = new[]
        {
            new Vector3(0, 25f/2f, 0),
            new Vector3(45f, 25f/2f, 0),
            new Vector3(45f/2f, 0, 0),
            new Vector3(45f/2f, 25f, 0),
        };
    }
    
    public void Spawn()
    {
        int count = 5;//MAGIC NUMBER
        for (int i = 0; i < count; i++)
        {
            var randomEnemy = _variants[_random.Next(0, _variants.Length)];
            _enemySystem.Work(randomEnemy.Invoke());
        }
    }

    private ShootingEnemy CreateShootingEnemy()
    {
        var position = _spawnPositions[_random.Next(0, _spawnPositions.Length)];
        ShootingEnemy enemy = new ShootingEnemy(new List<IComponent>
        {
            new Transform
            {
                Position = position
            },
            new RigidBody2D()
        }, Layer.Enemy);
        enemy.AddComponent(new SpriteRenderer(_dx2D, "shootingEnemy.png"));
        enemy.AddComponent(new BoxCollider2D(_collisionController, enemy, new Size(2, 2)));
        return enemy;
    }
}