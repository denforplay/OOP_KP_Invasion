using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Graphics;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
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
    private IGraphicProvider _graphicProvider;
    private bool _isSpawning = true;
    public EnemySpawner(EnemySystem enemySystem, CollisionController controller, IGraphicProvider graphicProvider)
    {
        _graphicProvider = graphicProvider;
        _collisionController = controller;
        _enemySystem = enemySystem;
        _variants = new Func<EnemyBase>[]
        {
            new ShootingEnemyFactory(_graphicProvider, _collisionController, new EnemyConfiguration(2, 1f, 1)).Create,
            new BeatingEnemyFactory(_graphicProvider, _collisionController, new EnemyConfiguration(health:8, speed:1.25f, cost:1)).Create,
            new KamikadzeEnemyFactory(_graphicProvider, _collisionController, new EnemyConfiguration(health:2, speed:3f, cost:1)).Create
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
        while (_isSpawning)
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

    public void StartSpawn()
    {
        _isSpawning = true;
    }

    public void StopSpawn()
    {
        _isSpawning = false;
    }
}