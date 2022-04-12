using Invasion.Engine;
using Invasion.Models;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators;
using Invasion.Models.Spawners;
using Invasion.Models.Systems;
using Invasion.View.Factories.Base;
using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot.Implementations;

public class ModificatorsCompositeRoot : ICompositeRoot
{
    private DX2D _dx2D;
    private ModificatorSystem _modificatorSystem;
    private ModificationSpawner _modificationSpawner;
    private ModificatorFactory _viewFactory;
    private Scene _gameScene;

    public ModificatorsCompositeRoot(DX2D dx2D, CollisionController collisionController, Scene gameScene, ModificatorSystem modificatorSystem)
    {
        _modificatorSystem = modificatorSystem;
        _gameScene = gameScene;
        _dx2D = dx2D;
        _modificationSpawner = new ModificationSpawner(_modificatorSystem, collisionController, _dx2D);
        _viewFactory = new ModificatorFactory();

    }
    
    public void Compose()
    {
        _modificatorSystem.OnStart += SpawnModificator;
        _modificatorSystem.OnEnd += DeleteModificator;
        _modificationSpawner.Spawn();
    }

    private void SpawnModificator(Entity<ModificatorBase> modificator)
    {
        var modificatorView = _viewFactory.Create(modificator);
        _gameScene.AddGameObject(modificator.GetEntity);
        _gameScene.AddGameObjectView(modificatorView);
        modificator.GetEntity.OnDestroyed += () =>
        {
            _gameScene.RemoveGameObjectView(modificatorView);
            _gameScene.RemoveGameObject(modificator.GetEntity);
        };
    }
    
    private void DeleteModificator(Entity<ModificatorBase> modificator)
    {
        modificator.GetEntity.OnDestroy();
        _viewFactory.Destroy(modificator);
    }

    public void Dispose()
    {
        _modificationSpawner.StopSpawn();
    }
}