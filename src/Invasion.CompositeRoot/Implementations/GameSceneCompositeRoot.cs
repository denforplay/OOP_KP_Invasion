using Invasion.Core.EventBus;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.InputSystem;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using Invasion.Models.Events;
using Invasion.Models.Systems;
using Invasion.View;
using Invastion.CompositeRoot.Base;
using SharpDX;
using Image = Invasion.Engine.Components.Image;
using RectangleF = SharpDX.RectangleF;
using Size = System.Drawing.Size;

namespace Invastion.CompositeRoot.Implementations
{
    public class GameSceneCompositeRoot : ICompositeRoot
    {
        bool _isDisposed;
        private CollisionsCompositeRoot _collisionsRoot;
        private HeroCompositeRoot _heroCompositeRoot;
        private EnemyCompositeRoot _enemyCompositeRoot;
        private ModificatorsCompositeRoot _modificatorsCompositeRoot;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        private ModificatorSystem _modificatorSystem;
        private Scene _gameScene;
        private DX2D _dx2d;
        private readonly DInput _dInput;
        private RectangleF _clientRect;
        private Image _background;
        private ScoreSystem _scoreSystem;
        private Dictionary<string, Type> _playerWeapons;
        private GameConfiguration _gameConfiguration;
        
        public GameSceneCompositeRoot(DX2D dX2D, DInput dInput, RectangleF clientRect, Dictionary<string, Type> playerWeapons, GameConfiguration gameConfiguration)
        {
            _playerWeapons = playerWeapons;
            _clientRect = clientRect;
            _dInput = dInput;
            _dx2d = dX2D;
            _gameScene = new Scene();
            _bulletSystem = new BulletSystem();
            _enemySystem = new EnemySystem();
            _modificatorSystem = new ModificatorSystem();
            _gameConfiguration = gameConfiguration;
            Compose();
        }

        public void Compose()
        {
            _dx2d.LoadBitmap(@"Sources\background.bmp");
            _dx2d.LoadBitmap(@"Sources\dash.bmp");
            _dx2d.LoadBitmap(@"Sources\topdownwall.png");
            _dx2d.LoadBitmap(@"Sources\pistol.png");
            _dx2d.LoadBitmap(@"Sources\knife.png");
            _dx2d.LoadBitmap(@"Sources\defaultBullet.png");
            _dx2d.LoadBitmap(@"Sources\shootingEnemy.png");
            _dx2d.LoadBitmap(@"Sources\speedBonus.png");
            _dx2d.LoadBitmap(@"Sources\slowTrap.png");
            _dx2d.LoadBitmap(@"Sources\kamikadzeEnemy.png");
            _dx2d.LoadBitmap(@"Sources\beatingEnemy.png");
            _background = new Image(_dx2d, @"Sources\background.bmp");
            _collisionsRoot = new CollisionsCompositeRoot(_bulletSystem, _enemySystem, _modificatorSystem);
            _collisionsRoot.Compose();
            _heroCompositeRoot = new HeroCompositeRoot(_dInput, _dx2d, _bulletSystem, _collisionsRoot, _clientRect, _gameScene, _playerWeapons);
            _heroCompositeRoot.Compose();
            _enemyCompositeRoot = new EnemyCompositeRoot(_dx2d, _bulletSystem, _enemySystem, _clientRect, 
                _collisionsRoot.Controller, _gameScene, _heroCompositeRoot.Players);
            _enemyCompositeRoot.Compose();
            _modificatorsCompositeRoot =
                new ModificatorsCompositeRoot(_dx2d, _collisionsRoot.Controller, _gameScene, _clientRect, _modificatorSystem);
            _modificatorsCompositeRoot.Compose();
            _scoreSystem = new ScoreSystem();
            var scoreView = new ScoreView(_scoreSystem, _dx2d.RenderTarget);
            _gameScene.AddGameObjectView(scoreView);
            _enemySystem.OnEnd += UpdateScoreSystem;
            GenerateBorders();
        }

        private void UpdateScoreSystem(Entity<EnemyBase> enemy)
        {
            _scoreSystem.AddScores(enemy.GetEntity.Cost);
            if (_scoreSystem.CurrentScore >= _gameConfiguration.NeededScore)
            {
                SingletonEventBus.GetInstance.Invoke(new GameWinEvent(_scoreSystem.CurrentScore));
            }
        }

        private void GenerateBorders()
        {
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(20, 0.8f, 0), Vector3.Zero, new Size(1920, 2));
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(20, 25f, 0),Vector3.Zero, new Size(1920, 2));
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(0f, 15f, 0), new Vector3((float)Math.PI/2, 0, 0), new Size(2, 1080));
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(44.5f, 15f, 0), new Vector3((float)Math.PI/2, 0, 0), new Size(2, 1080));
        }

        private void GenerateBorder(string spriteName, Vector3 position, Vector3 rotation, Size size)
        {
            var border = new Border(new List<IComponent>
            {
                new Transform
                {
                    Position = position,
                    Rotation = rotation
                },
                new RigidBody2D
                {
                },
                new SpriteRenderer(_dx2d, spriteName),
            });

            border.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, border, size));
            var borderView = new GameObjectView(border, _clientRect.Height / 25f, _clientRect.Height);
            _gameScene.AddGameObject(border);
            _gameScene.AddGameObjectView(borderView);
        }

        public void Update(float scale)
        {
            if (!_isDisposed)
            {
                _collisionsRoot.Update();
                _background.DrawBackground(1, _clientRect.Height / 25f, 25f / 1080, _clientRect.Height);
                _gameScene.FixedUpdate();
                _gameScene.Update();
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
            _enemyCompositeRoot.Dispose();
            _modificatorsCompositeRoot.Dispose();
            _bulletSystem.StopAll();
            _enemySystem.StopAll();
            _modificatorSystem.StopAll();
            _gameScene.Dispose();
        }
    }
}
