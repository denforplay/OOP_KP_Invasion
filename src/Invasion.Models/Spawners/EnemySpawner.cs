using Invasion.Models.Enemies;
using Invasion.Models.Systems;

namespace Invasion.Models.Spawners;

public class EnemySpawner
{
    private EnemySystem _enemySystem;
    private readonly Func<EnemyBase>[] _variants;
    private readonly Random _random = new Random();
    public EnemySpawner(EnemySystem enemySystem)
    {
        _enemySystem = enemySystem;
        _variants = new Func<EnemyBase>[]
        {

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
}