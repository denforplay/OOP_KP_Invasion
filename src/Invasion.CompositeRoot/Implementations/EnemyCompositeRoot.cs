using Invasion.Controller.Controllers;
using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Models;
using Invasion.Models.Collisions;
using Invasion.Models.Enemies;
using Invasion.Models.Spawners;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.View;
using Invasion.View.Factories.BulletFactories;
using Invasion.View.Factories.EnemyFactories;
using Invasion.View.Factories.EnemyFactories.Concretes;
using Invastion.CompositeRoot.Base;
using SharpDX;

namespace Invastion.CompositeRoot.Implementations;

public class EnemyCompositeRoot : ICompositeRoot
{
    private EnemySystem _enemySystem;
    private BulletSystem _bulletSystem;
    private EnemyFactory _enemyFactory;
    private EnemySpawner _enemySpawner;
    private CollisionController _collisionController;
    private Scene _gameScene;
    private List<Player> _players;

    private RectangleF _rectangle;
    private DX2D _dx2D;

    public EnemySystem EnemySystem => _enemySystem;

    public EnemyCompositeRoot(DX2D dx2D, BulletSystem bulletSystem, RectangleF rectangle, CollisionController collisionController, Scene gameScene, List<Player> players)
    {
        _bulletSystem = bulletSystem;
        _players = players;
        _rectangle = rectangle;
        _gameScene = gameScene;
        _collisionController = collisionController;
        _dx2D = dx2D;
    }
    
    public void Compose()
    {
        _enemySystem = new EnemySystem();
        _enemyFactory = new EnemyFactory(_dx2D, new ShootingEnemyFactory());
        _enemySpawner = new EnemySpawner(_enemySystem, _collisionController, _dx2D);
        _enemySystem.OnStart += SpawnEnemy;
        _enemySpawner.Spawn();
    }

    private void SpawnEnemy(Entity<EnemyBase> enemy)
    {
        var enemyView = _enemyFactory.Create(enemy, _rectangle.Height / 25f, _rectangle.Height);
        var weapon = new Pistol(_dx2D, new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_dx2D, "pistol.png"),
        }, enemy.GetEntity);
        
        var weaponView =
            new GameObjectView(weapon, _rectangle.Height / 25F, _rectangle.Height);
        _gameScene.AddGameObject(weapon as GameObject);
        _gameScene.AddGameObjectView(weaponView);
        weapon.OnShotEvent += Shoot;
        var controller = new EnemyController(enemy.GetEntity, _players);
        controller.BindGun(weapon);
        _gameScene.AddGameObject(enemy.GetEntity);
        _gameScene.AddGameObjectView(enemyView);
        _gameScene.AddController(controller);
    }

    private void Shoot(BulletBase bullet)
    {
        bullet.AddComponent(new BoxCollider2D(_collisionController, bullet, new System.Drawing.Size(1, 1)));
        _bulletSystem.Work(bullet);
    }
}