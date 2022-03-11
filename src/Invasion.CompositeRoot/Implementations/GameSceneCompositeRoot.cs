using System.Drawing;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.InputSystem;
using Invasion.View;
using Invastion.CompositeRoot.Base;
using SharpDX;
using SharpDX.Windows;
using Image = Invasion.Engine.Components.Image;
using RectangleF = SharpDX.RectangleF;

namespace Invastion.CompositeRoot.Implementations
{
    public class GameSceneCompositeRoot : ICompositeRoot
    {
        private CollisionsCompositeRoot _collisionsRoot;
        private HeroCompositeRoot _heroCompositeRoot;
        private Scene _gameScene;
        private DX2D _dx2d;
        private readonly DInput _dInput;
        private RectangleF _clientRect;
        private readonly RenderForm _renderForm;
        private Image _background;

        public GameSceneCompositeRoot(DX2D dX2D, DInput dInput, RenderForm renderForm, RectangleF clientRect)
        {
            _clientRect = clientRect;
            _collisionsRoot = new CollisionsCompositeRoot();
            _dInput = dInput;
            _dx2d = dX2D;
            _renderForm = renderForm;
            _gameScene = new Scene();
            _heroCompositeRoot = new HeroCompositeRoot(_renderForm, _dInput, _dx2d, _collisionsRoot, clientRect, _gameScene);
            Compose();
        }

        public void Compose()
        {
            _dx2d.LoadBitmap("background.bmp");
            _dx2d.LoadBitmap("dash.bmp");
            _dx2d.LoadBitmap("topdownwall.png");
            _dx2d.LoadBitmap("leftrightwall.png");
            _dx2d.LoadBitmap("pistol.png");
            _dx2d.LoadBitmap("defaultBullet.png");
            _background = new Image(_dx2d, "background.bmp");
            _heroCompositeRoot.Compose();
            GenerateBorders();
        }

        private void GenerateBorders()
        {
            GenerateBorder("topdownwall.png", new Vector3(15f, 0.8f, 0), new Size(1920, 2));
            GenerateBorder("topdownwall.png", new Vector3(22.25f, 25f, 0), new Size(1920, 2));
            
            GenerateBorder("leftrightwall.png", new Vector3(0f, 15f, 0), new Size(2, 1080));
            GenerateBorder("leftrightwall.png", new Vector3(44.5f, 15f, 0), new Size(2, 1080));
        }

        private void GenerateBorder(string spriteName, Vector3 position, Size size)
        {
            var border = new GameObject(new List<Invasion.Core.Interfaces.IComponent>
            {
                new Transform
                {
                    Position = position
                },
                new RigidBody2D
                {
                },
                new SpriteRenderer(_dx2d, spriteName),
            }, Layer.Border);

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
