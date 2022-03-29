using System.Drawing;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using Invasion.Models.Factories.EnemiesFactories;
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
            new ShootingEnemyFactory(_dx2D, _collisionController).Create,
            new BeatingEnemyFactory(_dx2D, _collisionController).Create,
            new KamikadzeEnemyFactory(_dx2D, _collisionController).Create,
        };
        _spawnPositions = new[]
        {
            new Vector3(0, 25f/2f, 0),
            new Vector3(45f, 25f/2f, 0),
            new Vector3(45f/2f, 0, 0),
            new Vector3(45f/2f, 25f, 0),
        };
    }
    
    public async void Spawn()
    {
        while (true)
        {
            if (_enemySystem.Entities.Count() <= 50)
            {
                var randomEnemy = _variants[_random.Next(0, _variants.Length)].Invoke();
                var randomPosition = _spawnPositions[_random.Next(0, _spawnPositions.Length)];
                if (randomEnemy.TryTakeComponent(out Transform transform))
                {
                    transform.Position = randomPosition;
                }

                _enemySystem.Work(randomEnemy);
            }
            await Task.Delay(3000);
        }
    }
}