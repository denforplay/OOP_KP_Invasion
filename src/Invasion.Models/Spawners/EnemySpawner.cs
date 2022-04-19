using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Collisions;
using Invasion.Models.Enemies;
using Invasion.Models.Factories.EnemiesFactories;
using Invasion.Models.Interfaces;
using Invasion.Models.Systems;
using System.Numerics;

namespace Invasion.Models.Spawners;

public class EnemySpawner : ISpawner
{
    private EnemySystem _enemySystem;
    private readonly Func<EnemyBase>[] _variants;
    private readonly Random _random = new Random();
    private readonly CollisionController _collisionController;
    private Vector3[] _spawnPositions;
    private DirectXGraphicsProvider _dx2D;
    private bool _isSpawning = true;
    public EnemySpawner(EnemySystem enemySystem, CollisionController controller, DirectXGraphicsProvider dx2D)
    {
        _dx2D = dx2D;
        _collisionController = controller;
        _enemySystem = enemySystem;
        _variants = new Func<EnemyBase>[]
        {
            new ShootingEnemyFactory(_dx2D, _collisionController).Create,
            new BeatingEnemyFactory(_dx2D, _collisionController).Create,
            new KamikadzeEnemyFactory(_dx2D, _collisionController).Create
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
        while (_isSpawning && Time.TimeScale != 0)
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

    public void StopSpawn()
    {
        _isSpawning = false;
    }
}