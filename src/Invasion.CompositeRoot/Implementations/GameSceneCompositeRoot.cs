using System.Drawing;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.InputSystem;
using Invasion.Models;
using Invasion.Models.Systems;
using Invasion.View;
using Invastion.CompositeRoot.Base;
using SharpDX;
using Image = Invasion.Engine.Components.Image;
using RectangleF = SharpDX.RectangleF;

namespace Invastion.CompositeRoot.Implementations
{
    public class GameSceneCompositeRoot : ICompositeRoot
    {
        private CollisionsCompositeRoot _collisionsRoot;
        private HeroCompositeRoot _heroCompositeRoot;
        private EnemyCompositeRoot _enemyCompositeRoot;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        private Scene _gameScene;
        private DX2D _dx2d;
        private readonly DInput _dInput;
        private RectangleF _clientRect;
        private Image _background;
        public GameSceneCompositeRoot(DX2D dX2D, DInput dInput, RectangleF clientRect)
        {
            _clientRect = clientRect;
            _dInput = dInput;
            _dx2d = dX2D;
            _gameScene = new Scene();
            _bulletSystem = new BulletSystem();
            _enemySystem = new EnemySystem();
            Compose();
        }

        public void Compose()
        {
            _dx2d.LoadBitmap("background.bmp");
            _dx2d.LoadBitmap("dash.bmp");
            _dx2d.LoadBitmap("topdownwall.png");
            _dx2d.LoadBitmap("leftrightwall.png");
            _dx2d.LoadBitmap("pistol.png");
            _dx2d.LoadBitmap("knife.png");
            _dx2d.LoadBitmap("defaultBullet.png");
            _dx2d.LoadBitmap("shootingEnemy.png");
            _background = new Image(_dx2d, "background.bmp");
            _collisionsRoot = new CollisionsCompositeRoot(_bulletSystem, _enemySystem);
            _heroCompositeRoot = new HeroCompositeRoot(_dInput, _dx2d, _bulletSystem, _collisionsRoot, _clientRect, _gameScene);
            _heroCompositeRoot.Compose();
            _enemyCompositeRoot = new EnemyCompositeRoot(_dx2d, _bulletSystem, _enemySystem, _clientRect, 
                _collisionsRoot.Controller, _gameScene, _heroCompositeRoot.Players);
            _enemyCompositeRoot.Compose();
            GenerateBorders();
        }

        private void GenerateBorders()
        {
            GenerateBorder("topdownwall.png", new Vector3(20, 0.8f, 0), Vector3.Zero, new Size(1920, 2));
            GenerateBorder("topdownwall.png", new Vector3(20, 25f, 0),Vector3.Zero, new Size(1920, 2));
            GenerateBorder("topdownwall.png", new Vector3(0f, 15f, 0), new Vector3((float)Math.PI/2, 0, 0), new Size(2, 1080));
            GenerateBorder("topdownwall.png", new Vector3(44.5f, 15f, 0), new Vector3((float)Math.PI/2, 0, 0), new Size(2, 1080));
        }

        private void GenerateBorder(string spriteName, Vector3 position, Vector3 rotation, Size size)
        {
            var border = new Border(new List<Invasion.Core.Interfaces.IComponent>
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
            _collisionsRoot.Update();
            _background.DrawBackground(1, _clientRect.Height / 25f, 25f/1080, _clientRect.Height);
            _gameScene.Update();
            _gameScene.FixedUpdate();
        }
    }
}
