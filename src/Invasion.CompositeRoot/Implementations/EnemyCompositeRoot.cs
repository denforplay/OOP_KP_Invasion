using Invasion.Controller.Controllers;
using Invasion.Engine;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Collisions;
using Invasion.Models.Enemies;
using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Spawners;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Melee;
using Invasion.View;
using Invasion.View.Factories.Base;
using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot.Implementations;

/// <summary>
/// Compose enemies
/// </summary>
public class EnemyCompositeRoot : ICompositeRoot
{
    private EnemySystem _enemySystem;
    private GameObjectViewFactoryBase<EnemyBase> _enemyFactory;
    private EnemySpawner _enemySpawner;
    private CollisionController _collisionController;
    private Scene _gameScene;
    private List<Player> _players;
    private IGraphicProvider _graphicProvider;
    private WeaponFactory _weaponFactory;
    private Dictionary<Type, Type> _enemyWeapons = new Dictionary<Type, Type>
    {
        { typeof(ShootingEnemy), typeof(Pistol) },
        { typeof(BeatingEnemy), typeof(Knife) },
        {typeof(KamikadzeEnemy),typeof(EmptyWeapon) },
    };

    /// <summary>
    /// Enemy composite root constructor
    /// </summary>
    /// <param name="graphicsProvider">Graphics provider</param>
    /// <param name="bulletSystem">Bullet system</param>
    /// <param name="enemySystem">Enemy system</param>
    /// <param name="collisionController">Collision controller</param>
    /// <param name="gameScene">Game scene</param>
    /// <param name="players">Game players</param>
    public EnemyCompositeRoot(IGraphicProvider graphicProvider, BulletSystem bulletSystem, EnemySystem enemySystem,
        CollisionController collisionController, Scene gameScene, List<Player> players)
    {
        _enemySystem = enemySystem;
        _players = players;
        _gameScene = gameScene;
        _collisionController = collisionController;
        _graphicProvider = graphicProvider;
        _weaponFactory = new WeaponFactory(_collisionController, bulletSystem, graphicProvider);
    }

    public void Compose()
    {
        _enemyFactory = new GameObjectViewFactoryBase<EnemyBase>();
        _enemySpawner = new EnemySpawner(_enemySystem, _collisionController, _graphicProvider);
        _enemySystem.OnStart += SpawnEnemy;
        _enemySystem.OnEnd += DeleteEnemy;
        _enemySpawner.Spawn();
    }

    /// <summary>
    /// Spawn enemy
    /// </summary>
    /// <param name="enemy">Enemy entity</param>
    private void SpawnEnemy(Entity<EnemyBase> enemy)
    {
        var enemyView = _enemyFactory.Create(enemy);
        var enemyWeapon = _weaponFactory.Create(enemy.GetEntity, _enemyWeapons[enemy.GetEntity.GetType()]);
        enemyWeapon = new WeaponBaseDecorator(enemyWeapon, enemyWeapon.Configuration, new List<IComponent>(enemyWeapon.Components));
        var collider = new BoxCollider2D(_collisionController, enemyWeapon, new System.Drawing.Size(2, 2));
        enemyWeapon.AddComponent(collider);
        var weaponView =
            new GameObjectView(enemyWeapon);
        _gameScene.AddGameObject(enemyWeapon);
        _gameScene.AddView(weaponView);
        var controller = new EnemyController(enemy.GetEntity, _players);
        controller.BindGun(enemyWeapon);
        var healthView = new HealthView(enemy.GetEntity, _graphicProvider.GraphicTarget);
        _gameScene.AddGameObject(enemy.GetEntity);
        _gameScene.AddView(enemyView);
        _gameScene.AddView(healthView);
        _gameScene.AddController(controller);
        enemy.GetEntity.OnDestroyed += () =>
        {
            enemyView.Dispose();
            _gameScene.RemoveController(controller);
            _gameScene.RemoveView(enemyView);
            _gameScene.RemoveView(healthView);
            _gameScene.RemoveGameObject(enemy.GetEntity);
            _gameScene.RemoveGameObject(enemyWeapon);
            _gameScene.RemoveView(weaponView);
        };
    }

    /// <summary>
    /// Delete enemy
    /// </summary>
    /// <param name="enemy">Enemy entity</param>
    private void DeleteEnemy(Entity<EnemyBase> enemy)
    {
        enemy.GetEntity.OnDestroy();
        _enemyFactory.Destroy(enemy);
    }

    public void Dispose()
    {
        _enemySpawner.StopSpawn();
    }

    /// <summary>
    /// Restart enemies systems
    /// </summary>
    public void Restart()
    {
        _enemySpawner.StartSpawn();
    }
}