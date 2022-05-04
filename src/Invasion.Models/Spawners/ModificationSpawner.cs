using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Graphics;
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
    private IGraphicProvider _graphicProvider;
    private bool _isSpawning = true;
    
    public ModificationSpawner(ModificatorSystem modificatorSystem, CollisionController controller, IGraphicProvider graphicProvider)
    {
        _graphicProvider = graphicProvider;
        _modificatorSystem = modificatorSystem;
        _variants = new Func<ModificatorBase>[]
        {
            new HigherReloadWeaponBonusFactory(_graphicProvider, controller).Create,
            new SlowTrapFactory(_graphicProvider, controller).Create,
            new LowerReloadWeaponBonusFactory(_graphicProvider, controller).Create,
            new HigherFireRateBonusFactory(graphicProvider, controller).Create,
            new FreezeTrapFactory(_graphicProvider, controller).Create,
            new CantShootTrapFactory(_graphicProvider, controller).Create
        };
    }
    
    public async void Spawn()
    {
        while (_isSpawning)
        {
            if (_modificatorSystem.Entities.Count() <= 5)
            {
                var randomX = (float)(_random.NextDouble() * (43f - 2f)) + 2f;
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

    public void StartSpawn()
    {
        _isSpawning = true;
    }
}