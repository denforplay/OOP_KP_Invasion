using Invasion.Controller.Controllers;
using Invasion.Controller.Inputs;
using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.InputSystem;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Factory;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.Models.Weapons.Melee;
using Invasion.View;
using Invasion.View.Factories;
using Invasion.View.Factories.Base;
using Invastion.CompositeRoot.Base;
using SharpDX;
using SharpDX.DirectInput;
using Size = System.Drawing.Size;

namespace Invastion.CompositeRoot.Implementations;

public class HeroCompositeRoot : ICompositeRoot
{
    private DInput _dInput;
    private DX2D _dx2D;
    private CollisionsCompositeRoot _collisionsRoot;
    private RectangleF _clientRect;
    private Scene _gameScene;
    private Dictionary<string, Player> _players = new Dictionary<string, Player>();
    private Dictionary<Player, (PlayerController, GameObjectView)> _playersData = new Dictionary<Player, (PlayerController, GameObjectView)>();
    private Dictionary<IWeapon, (GameObject, GameObjectView)> _weaponData = new Dictionary<IWeapon, (GameObject, GameObjectView)>();
    private GameObjectViewFactoryBase<BulletBase> _bulletFactory;
    private BulletSystem _bulletSystem;
    private PlayerConfiguration _playerConfiguration;
    private WeaponFactory _weaponFactory;
    public List<Player> Players => _players.Values.ToList();
    public HeroCompositeRoot(DInput dInput, DX2D dx2D, BulletSystem bulletSystem, CollisionsCompositeRoot collisionsRoot, RectangleF clientRect, Scene gameScene)
    {
        _bulletSystem = bulletSystem;
        _dInput = dInput;
        _dx2D = dx2D;
        _collisionsRoot = collisionsRoot;
        _clientRect = clientRect;
        _gameScene = gameScene;
        _bulletFactory = new DefaultBulletFactory();
        _playerConfiguration = new PlayerConfiguration(1, 5);
        _bulletSystem.OnStart += SpawnBullet;
        _bulletSystem.OnEnd += DeleteBullet;
        _weaponFactory = new WeaponFactory(_collisionsRoot.Controller, bulletSystem, dx2D);
    }

    public void Compose()
    {
       CreatePlayer("firstPlayer", "dash.bmp", new Vector3(15f, 15f, 0f), new Size(2, 2), new PlayerInput(_dInput, Key.W, Key.S, Key.D, Key.A));
       CreatePlayer("secondPlayer", "dash.bmp", new Vector3(5f, 5f, 0f), new Size(2, 2), new PlayerInput(_dInput, Key.NumberPad8, Key.NumberPad2, Key.NumberPad6, Key.NumberPad4));
       InitializeWeapons();
    }
    
    private void CreatePlayer(string playerTag, string sprite, Vector3 startPosition, Size colliderSize, PlayerInput inputs)
    {
        var player = new Player(new List<IComponent>
        {
            new Transform
            {
                Position = startPosition
            },
            new SpriteRenderer(_dx2D, sprite),
            new RigidBody2D()
        }, _playerConfiguration, Layer.Player);
        player.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, player, colliderSize));
        var playerView = new GameObjectView(player, _clientRect.Height / 25f, _clientRect.Height);
        var playerController = new PlayerController(player, inputs);
        var healthView = new HealthView(player, _dx2D.RenderTarget);
        _gameScene.Add(player, playerView, playerController);
        _gameScene.AddGameObjectView(healthView);
        _players.Add(playerTag, player);
        _playersData.Add(player, (playerController, playerView));
    }
    
    private void InitializeWeapons()
    {
        CreateWeapon<Pistol>(_players["firstPlayer"], new WeaponInput(_dInput, Key.Q, Key.E, Key.Space));
        CreateWeapon<Knife>(_players["secondPlayer"], new WeaponInput(_dInput, Key.NumberPad7, Key.NumberPad9, Key.NumberPadEnter));
    }
    
    private void CreateWeapon<T>(Player owner, WeaponInput weaponInput) where T: IWeapon
    {
        IWeapon weapon = _weaponFactory.Create<T>(owner);
            
        var weaponView =
            new GameObjectView(weapon as GameObject, _clientRect.Height / 25F, _clientRect.Height);
            
        _gameScene.AddGameObject(weapon as GameObject);
        _gameScene.AddGameObjectView(weaponView);
        _playersData[owner].Item1.BindGun(weapon, weaponInput);
    }
    
    private void SpawnBullet(Entity<BulletBase> bullet)
    {
        var bulletView = _bulletFactory.Create(bullet, _clientRect.Height / 25f, _clientRect.Height);
        _gameScene.AddGameObjectView(bulletView);
        _gameScene.AddGameObject(bullet.GetEntity);
        bullet.GetEntity.OnDestroyed += () =>
        {
            _gameScene.RemoveGameObject(bullet.GetEntity);
            _gameScene.RemoveGameObjectView(bulletView);
        };
    }
        
    private void DeleteBullet(Entity<BulletBase> bullet)
    {
        bullet.GetEntity.OnDestroy();
        _bulletFactory.Destroy(bullet);
    }
}