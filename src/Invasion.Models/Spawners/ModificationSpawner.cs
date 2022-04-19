using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Collisions;
using Invasion.Models.Factories.ModificatorFactories;
using Invasion.Models.Interfaces;
using Invasion.Models.Modificators;
using Invasion.Models.Systems;
using System.Numerics;

namespace Invasion.Models.Spawners;

public class ModificationSpawner : ISpawner
{
    private ModificatorSystem _modificatorSystem;
    private readonly Func<ModificatorBase>[] _variants;
    private readonly Random _random = new Random();
    private DirectXGraphicsProvider _dx2D;
    private bool _isSpawning = true;
    
    public ModificationSpawner(ModificatorSystem modificatorSystem, CollisionController controller, DirectXGraphicsProvider dx2D)
    {
        _dx2D = dx2D;
        _modificatorSystem = modificatorSystem;
        _variants = new Func<ModificatorBase>[]
        {
            new SpeedBonusFactory(_dx2D, controller).Create,
            new SlowTrapFactory(_dx2D, controller).Create,
        };
    }
    
    public async void Spawn()
    {
        while (_isSpawning && Time.TimeScale != 0)
        {
            if (_modificatorSystem.Entities.Count() <= 5)
            {
                var randomX = (float)(_random.NextDouble() * (43f-2f)) + 2f;
                var randomY = (float)(_random.NextDouble() * (23f - 2f)) + 2f;
                SpawnModificator(new Vector3(randomX, randomY, 0));
                await Task.Delay(1000);
            }
        }
    }

    public async void SpawnModificator(Vector3 position)
    {
        ModificatorBase randomModificator = _variants[_random.Next(0, _variants.Length)].Invoke();
        if (randomModificator.TryTakeComponent(out Transform transform))
        {
            transform.Position = position;
        }

        _modificatorSystem.Work(randomModificator);
        await Task.Delay(_random.Next(3000, 6000));
        _modificatorSystem.StopWork(randomModificator);
    }
    
    public void StopSpawn()
    {
        _isSpawning = false;
    }
}