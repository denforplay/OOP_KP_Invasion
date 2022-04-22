using Invasion.Controller.Controllers;
using Invasion.Controller.Inputs;
using Invasion.Core.EventBus;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Graphics;
using Invasion.Engine.InputSystem;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;
using Invasion.Models.Events;
using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.View;
using Invasion.View.Factories.Base;
using Invastion.CompositeRoot.Base;
using System.Drawing;
using System.Numerics;
using System.Windows.Input;

namespace Invastion.CompositeRoot.Implementations;

/// <summary>
/// Hero composite root
/// </summary>
public class HeroCompositeRoot : ICompositeRoot
{
    private IInputProvider _inputProvider;
    private IGraphicProvider _graphicsProvider;
    private CollisionsCompositeRoot _collisionsRoot;
    private Scene _gameScene;
    private Dictionary<string, Player> _players = new Dictionary<string, Player>();
    private Dictionary<Player, (PlayerController, GameObjectView)> _playersData = new Dictionary<Player, (PlayerController, GameObjectView)>();
    private Dictionary<WeaponBase, (GameObject, GameObjectView)> _weaponData = new Dictionary<WeaponBase, (GameObject, GameObjectView)>();
    private GameObjectViewFactoryBase<BulletBase> _bulletFactory;
    private BulletSystem _bulletSystem;
    private PlayerConfiguration _playerConfiguration;
    private WeaponFactory _weaponFactory;
    Dictionary<string, Type> _playerWeapons;

    /// <summary>
    /// Provides list of current game players
    /// </summary>
    public List<Player> Players => _players.Values.ToList();

    /// <summary>
    /// Hero composite root constructor
    /// </summary>
    /// <param name="dInput">DirectX input provider</param>
    /// <param name="dx2D">Graphics provider</param>
    /// <param name="bulletSystem">Bullet system</param>
    /// <param name="collisionsRoot">Collision composite root</param>
    /// <param name="gameScene">Game scene</param>
    /// <param name="playerWeapons">Player choosed weapons</param>
    public HeroCompositeRoot(IInputProvider inputProvider, IGraphicProvider graphicProvider, BulletSystem bulletSystem, CollisionsCompositeRoot collisionsRoot,Scene gameScene, Dictionary<string, Type> playerWeapons)
    {
        _playerWeapons = playerWeapons;
        _bulletSystem = bulletSystem;
        _inputProvider = inputProvider;
        _graphicsProvider = graphicProvider;
        _collisionsRoot = collisionsRoot;
        _gameScene = gameScene;
        _bulletFactory = new BulletFactory();
        _playerConfiguration = new PlayerConfiguration(1, 5);
        _bulletSystem.OnStart += SpawnBullet;
        _bulletSystem.OnEnd += DeleteBullet;
        _weaponFactory = new WeaponFactory(_collisionsRoot.Controller, bulletSystem, _graphicsProvider);
    }

    public void Compose()
    {
       CreatePlayer("Player1", @"Sources\character.png", new Vector3(15f, 15f, 0f), new Size(2, 2), new PlayerInput(_inputProvider, Key.W, Key.S, Key.D, Key.A));
       CreatePlayer("Player2", @"Sources\character.png", new Vector3(35f, 15f, 0f), new Size(2, 2), new PlayerInput(_inputProvider, Key.I, Key.K, Key.L, Key.J));
       InitializeWeapons();
    }
    
    /// <summary>
    /// Create player with sprite
    /// </summary>
    /// <param name="playerTag">Player tag</param>
    /// <param name="sprite">Player sprite</param>
    /// <param name="startPosition">Player start position</param>
    /// <param name="colliderSize">Player collider size</param>
    /// <param name="inputs">Player inputs
    /// </param>
    private void CreatePlayer(string playerTag, string sprite, Vector3 startPosition, Size colliderSize, PlayerInput inputs)
    {
        var player = new Player(new List<IComponent>
        {
            new Transform
            {
                Position = startPosition
            },
            new SpriteRenderer(_graphicsProvider, sprite),
            new RigidBody2D()
        }, _playerConfiguration, Layer.Player);
        var playerDecorator = new PlayerDecorator(player, new List<IComponent>(player.Components), _playerConfiguration);
        playerDecorator.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, playerDecorator, colliderSize));
        var playerView = new GameObjectView(playerDecorator);
        var playerController = new PlayerController(playerDecorator, inputs);
        var healthView = new HealthView(playerDecorator, _graphicsProvider.GraphicTarget);
        playerDecorator.OnDestroyed += () =>
        {
            SingletonEventBus.GetInstance.Invoke(new GameLoseEvent());
        };
        _gameScene.Add(playerDecorator, playerView, playerController);
        _gameScene.AddGameObjectView(healthView);
        _players.Add(playerTag, playerDecorator);
        _playersData.Add(playerDecorator, (playerController, playerView));
    }
    
    /// <summary>
    /// Initialize weapons
    /// </summary>
    private void InitializeWeapons()
    {
        CreateWeapon(_players["Player1"], new WeaponInput(_inputProvider, Key.Q, Key.E, Key.Space), _playerWeapons["Player1"]);
        CreateWeapon(_players["Player2"], new WeaponInput(_inputProvider, Key.U, Key.O, Key.Enter), _playerWeapons["Player2"]);
    }
    
    /// <summary>
    /// Create weapon method
    /// </summary>
    /// <param name="owner">Weapon owner</param>
    /// <param name="weaponInput">Weapon inputs</param>
    /// <param name="type">Type type</param>
    private void CreateWeapon(Player owner, WeaponInput weaponInput, Type type)
    {
        var weaponModel = _weaponFactory.Create(owner, type);
        WeaponBase weaponBase = new WeaponBaseDecorator(weaponModel, new List<IComponent>(weaponModel.Components));
        var collider = new BoxCollider2D(_collisionsRoot.Controller, weaponBase, new Size(2, 2));
        weaponBase.AddComponent(collider);
        var weaponView =
            new GameObjectView(weaponBase);
            
        _gameScene.AddGameObject(weaponBase);
        _gameScene.AddGameObjectView(weaponView);
        _playersData[owner].Item1.BindGun(weaponBase, weaponInput);
    }
    
    /// <summary>
    /// Spawn bullet from entity
    /// </summary>
    /// <param name="bullet">Bullet entity</param>
    private void SpawnBullet(Entity<BulletBase> bullet)
    {
        var bulletView = _bulletFactory.Create(bullet);
        _gameScene.AddGameObjectView(bulletView);
        _gameScene.AddGameObject(bullet.GetEntity);
        bullet.GetEntity.OnDestroyed += () =>
        {
            bulletView.Dispose();
            _gameScene.RemoveGameObject(bullet.GetEntity);
            _gameScene.RemoveGameObjectView(bulletView);
        };
    }
        
    /// <summary>
    /// Delete bullet from entity
    /// </summary>
    /// <param name="bullet">Bullet entity</param>
    private void DeleteBullet(Entity<BulletBase> bullet)
    {
        bullet.GetEntity.OnDestroy();
        _bulletFactory.Destroy(bullet);
    }

    public void Dispose()
    {
    }

    public void Restart()
    {
        _players["Player1"].TryTakeComponent(out Transform transform1);
        transform1.Position = new Vector3(15f, 15f, 0f);
        _players["Player2"].TryTakeComponent(out Transform transform2);
        _players["Player1"].SetHealth(_playerConfiguration.MaxHealth);
        _players["Player2"].SetHealth(_playerConfiguration.MaxHealth);
        transform2.Position = new Vector3(35f, 15f, 0f);
    }
}