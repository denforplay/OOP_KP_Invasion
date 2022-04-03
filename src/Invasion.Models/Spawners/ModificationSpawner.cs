using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Collisions;
using Invasion.Models.Factories.ModificatorFactories;
using Invasion.Models.Modificators;
using Invasion.Models.Systems;
using SharpDX;

namespace Invasion.Models.Spawners;

public class ModificationSpawner
{
    private ModificatorSystem _modificatorSystem;
    private readonly Func<ModificatorBase>[] _variants;
    private readonly Random _random = new Random();
    private DX2D _dx2D;
    private bool _isSpawning = true;
    
    public ModificationSpawner(ModificatorSystem modificatorSystem, CollisionController controller, DX2D dx2D)
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
        while (_isSpawning)
        {
            if (_modificatorSystem.Entities.Count() <= 5)
            {
                var randomX = _random.NextFloat(2f, 43f);
                var randomY = _random.NextFloat(2f, 23f);
                var randomBonus = _variants[_random.Next(0, _variants.Length)].Invoke();
                if (randomBonus.TryTakeComponent(out Transform transform))
                {
                    transform.Position = new Vector3(randomX, randomY, 0);
                }
                _modificatorSystem.Work(randomBonus);
                await Task.Delay(_random.Next(3000, 6000));
                _modificatorSystem.StopWork(randomBonus);
            }
        }
    }
    
    public void StopSpawn()
    {
        _isSpawning = false;
    }
}