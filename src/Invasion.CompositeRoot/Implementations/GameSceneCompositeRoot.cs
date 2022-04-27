using Invasion.Core.EventBus;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Components.Interfaces;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using Invasion.Models.Events;
using Invasion.Models.Systems;
using Invasion.View;
using Invastion.CompositeRoot.Base;
using System.Numerics;
using Size = System.Drawing.Size;

namespace Invastion.CompositeRoot.Implementations
{
    /// <summary>
    /// Game scene composite root
    /// </summary>
    public class GameSceneCompositeRoot : ICompositeRoot
    {
        private bool _isDisposed;
        private CollisionsCompositeRoot _collisionsRoot;
        private HeroCompositeRoot _heroCompositeRoot;
        private EnemyCompositeRoot _enemyCompositeRoot;
        private ModificatorsCompositeRoot _modificatorsCompositeRoot;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        private ModificatorSystem _modificatorSystem;
        private Scene _gameScene;
        private IRenderer _background;
        private ScoreSystem _scoreSystem;
        private Dictionary<string, Type> _playerWeapons;
        private GameConfiguration _gameConfiguration;
        private IInputProvider _inputProvider;
        private IGraphicProvider _graphicsProvider;

        /// <summary>
        /// Game scene composite root constructor
        /// </summary>
        /// <param name="dX2D">Graphic provider</param>
        /// <param name="inputProvider">Input helper</param>
        /// <param name="playerWeapons">Player weapons</param>
        /// <param name="gameConfiguration">Game configuration</param>
        public GameSceneCompositeRoot(IGraphicProvider graphicsProvider, IInputProvider inputProvider, Dictionary<string, Type> playerWeapons, GameConfiguration gameConfiguration)
        {
            _playerWeapons = playerWeapons;
            _inputProvider = inputProvider;
            _graphicsProvider = graphicsProvider;
            _gameScene = new Scene();
            _bulletSystem = new BulletSystem();
            _enemySystem = new EnemySystem();
            _modificatorSystem = new ModificatorSystem();
            _gameConfiguration = gameConfiguration;
            Compose();
        }

        public void Compose()
        {
            _graphicsProvider.LoadBitmap(@"Sources\background.bmp");
            _graphicsProvider.LoadBitmap(@"Sources\character.png");
            _graphicsProvider.LoadBitmap(@"Sources\topdownwall.png");
            _graphicsProvider.LoadBitmap(@"Sources\pistol.png");
            _graphicsProvider.LoadBitmap(@"Sources\knife.png");
            _graphicsProvider.LoadBitmap(@"Sources\defaultBullet.png");
            _graphicsProvider.LoadBitmap(@"Sources\shootingEnemy.png");
            _graphicsProvider.LoadBitmap(@"Sources\speedWeaponBonus.png");
            _graphicsProvider.LoadBitmap(@"Sources\slowedWeaponBonus.png");
            _graphicsProvider.LoadBitmap(@"Sources\removeHealthTrap.png");
            _graphicsProvider.LoadBitmap(@"Sources\lowerReload.png");
            _graphicsProvider.LoadBitmap(@"Sources\higherReload.png");
            _graphicsProvider.LoadBitmap(@"Sources\moreDamageBonus.png");
            _graphicsProvider.LoadBitmap(@"Sources\slowTrap.png");
            _graphicsProvider.LoadBitmap(@"Sources\kamikadzeEnemy.png");
            _graphicsProvider.LoadBitmap(@"Sources\beatingEnemy.png");
            _background = new SpriteRenderer(_graphicsProvider, @"Sources\background.bmp", RendererMode.Static);
            _collisionsRoot = new CollisionsCompositeRoot(_bulletSystem, _enemySystem, _modificatorSystem);
            _collisionsRoot.Compose();
            _heroCompositeRoot = new HeroCompositeRoot(_inputProvider, _graphicsProvider, _bulletSystem, _collisionsRoot, _gameScene, _playerWeapons);
            _heroCompositeRoot.Compose();
            _enemyCompositeRoot = new EnemyCompositeRoot(_graphicsProvider, _bulletSystem, _enemySystem,
                _collisionsRoot.Controller, _gameScene, _heroCompositeRoot.Players);
            _enemyCompositeRoot.Compose();
            _modificatorsCompositeRoot =
                new ModificatorsCompositeRoot(_graphicsProvider, _collisionsRoot.Controller, _gameScene, _modificatorSystem);
            _modificatorsCompositeRoot.Compose();
            _scoreSystem = new ScoreSystem();
            var scoreView = new ScoreView(_scoreSystem, _graphicsProvider.GraphicTarget.Target);
            _gameScene.AddGameObjectView(scoreView);
            _enemySystem.OnEnd += UpdateScoreSystem;
            GenerateBorders();
        }

        public void Restart()
        {
            _scoreSystem.Restart();
            _heroCompositeRoot.Restart();
            _enemyCompositeRoot.Restart();
            _modificatorsCompositeRoot.Restart();
            Time.TimeScale = 1;
        }

        public void StopGame()
        {
            _bulletSystem.StopAll();
            _enemySystem.StopAll();
            _modificatorSystem.StopAll();
            Time.TimeScale = 0;
        }

        /// <summary>
        /// Update score system
        /// </summary>
        /// <param name="enemy">Enemy entity</param>
        private void UpdateScoreSystem(Entity<EnemyBase> enemy)
        {
            _scoreSystem.AddScores(enemy.GetEntity.Cost);
            if (_scoreSystem.CurrentScore >= _gameConfiguration.NeededScore)
            {
                SingletonEventBus.GetInstance.Invoke(new GameWinEvent(_scoreSystem.CurrentScore));
            }
        }

        /// <summary>
        /// Generate borders
        /// </summary>
        private void GenerateBorders()
        {
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(20, 0.8f, 0), Vector3.Zero, new Size(1920, 2));
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(20, 25f, 0),Vector3.Zero, new Size(1920, 2));
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(0f, 15f, 0), new Vector3((float)Math.PI/2, 0, 0), new Size(2, 1080));
            GenerateBorder(@"Sources\topdownwall.png", new Vector3(44.5f, 15f, 0), new Vector3((float)Math.PI/2, 0, 0), new Size(2, 1080));
        }

        /// <summary>
        /// Generate border from sprite on position with rotation and size
        /// </summary>
        /// <param name="spriteName">Sprite name</param>
        /// <param name="position">Position</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="size">Size</param>
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
                new SpriteRenderer(_graphicsProvider, spriteName),
            });

            border.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, border, size));
            var borderView = new GameObjectView(border);
            _gameScene.AddGameObject(border);
            _gameScene.AddGameObjectView(borderView);
        }

        /// <summary>
        /// Update game
        /// </summary>
        /// <param name="scale">Scale (useful)</param>
        public void Update(float scale)
        {
            if (!_isDisposed)
            {
                _collisionsRoot.Update();
                _background.Draw();
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
