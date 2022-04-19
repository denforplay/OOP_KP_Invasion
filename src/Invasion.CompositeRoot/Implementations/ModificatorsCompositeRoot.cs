using Invasion.Engine;
using Invasion.Models;
using Invasion.Models.Collisions;
using Invasion.Models.Modificators;
using Invasion.Models.Spawners;
using Invasion.Models.Systems;
using Invasion.View.Factories.Base;
using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot.Implementations;

/// <summary>
/// Modificators composite root
/// </summary>
public class ModificatorsCompositeRoot : ICompositeRoot
{
    private DirectXGraphicsProvider _dx2D;
    private ModificatorSystem _modificatorSystem;
    private ModificationSpawner _modificationSpawner;
    private ModificatorFactory _viewFactory;
    private Scene _gameScene;

    /// <summary>
    /// Modificators composite root constructor
    /// </summary>
    /// <param name="dx2D">Graphic provider</param>
    /// <param name="collisionController">Collision controller</param>
    /// <param name="gameScene">Game scene</param>
    /// <param name="modificatorSystem">Modificator system</param>
    public ModificatorsCompositeRoot(DirectXGraphicsProvider dx2D, CollisionController collisionController, Scene gameScene, ModificatorSystem modificatorSystem)
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

    /// <summary>
    /// Spawn modificator
    /// </summary>
    /// <param name="modificator">Spawned modificator entity</param>
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
    
    /// <summary>
    /// Delete modificator
    /// </summary>
    /// <param name="modificator">Deleted modificator entity</param>
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