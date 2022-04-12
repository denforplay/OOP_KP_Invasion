using Invasion.Controller.Controllers;
using Invasion.Engine;
using Invasion.Engine.Components.Colliders;
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
using Invasion.View.Factories;
using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot.Implementations;

public class EnemyCompositeRoot : ICompositeRoot
{
    private EnemySystem _enemySystem;
    private EnemyFactory _enemyFactory;
    private EnemySpawner _enemySpawner;
    private CollisionController _collisionController;
    private Scene _gameScene;
    private List<Player> _players;
    private DX2D _dx2D;
    private WeaponFactory _weaponFactory;
    private Dictionary<Type, Type> _enemyWeapons = new Dictionary<Type, Type>
    {
        { typeof(ShootingEnemy), typeof(Pistol) },
        { typeof(BeatingEnemy), typeof(Knife) },
        {typeof(KamikadzeEnemy),typeof(EmptyWeapon) },
    };

    public EnemyCompositeRoot(DX2D dx2D, BulletSystem bulletSystem, EnemySystem enemySystem,
        CollisionController collisionController, Scene gameScene, List<Player> players)
    {
        _enemySystem = enemySystem;
        _players = players;
        _gameScene = gameScene;
        _collisionController = collisionController;
        _dx2D = dx2D;
        _weaponFactory = new WeaponFactory(_collisionController, bulletSystem, dx2D);
    }

    public void Compose()
    {
        _enemyFactory = new EnemyFactory();
        _enemySpawner = new EnemySpawner(_enemySystem, _collisionController, _dx2D);
        _enemySystem.OnStart += SpawnEnemy;
        _enemySystem.OnEnd += DeleteEnemy;
        _enemySpawner.Spawn();
    }

    private void SpawnEnemy(Entity<EnemyBase> enemy)
    {
        var enemyView = _enemyFactory.Create(enemy);
        var enemyWeapon = _weaponFactory.Create(enemy.GetEntity, _enemyWeapons[enemy.GetEntity.GetType()]);
        enemyWeapon = new WeaponBaseDecorator(enemyWeapon, new List<IComponent>(enemyWeapon.Components));
        var collider = new BoxCollider2D(_collisionController, enemyWeapon, new System.Drawing.Size(2, 2));
        enemyWeapon.AddComponent(collider);
        var weaponView =
            new GameObjectView(enemyWeapon);
        _gameScene.AddGameObject(enemyWeapon);
        _gameScene.AddGameObjectView(weaponView);
        var controller = new EnemyController(enemy.GetEntity, _players);
        controller.BindGun(enemyWeapon);
        var healthView = new HealthView(enemy.GetEntity, _dx2D.RenderTarget);
        _gameScene.AddGameObject(enemy.GetEntity);
        _gameScene.AddGameObjectView(enemyView);
        _gameScene.AddGameObjectView(healthView);
        _gameScene.AddController(controller);
        enemy.GetEntity.OnDestroyed += () =>
        {
            _gameScene.RemoveController(controller);
            _gameScene.RemoveGameObjectView(enemyView);
            _gameScene.RemoveGameObjectView(healthView);
            _gameScene.RemoveGameObject(enemy.GetEntity);
            _gameScene.RemoveGameObject(enemyWeapon);
            _gameScene.RemoveGameObjectView(weaponView);
        };
    }

    private void DeleteEnemy(Entity<EnemyBase> enemy)
    {
        enemy.GetEntity.OnDestroy();
        _enemyFactory.Destroy(enemy);
    }

    public void Dispose()
    {
        _enemySpawner.StopSpawn();
    }
}