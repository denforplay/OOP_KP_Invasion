using Invasion.Controller.Controllers;
using Invasion.Controller.Inputs;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.InputSystem;
using Invasion.View;
using Invastion.CompositeRoot.Base;
using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;

namespace Invastion.CompositeRoot.Implementations
{
    public class GameSceneCompositeRoot : ICompositeRoot
    {
        private CollisionsCompositeRoot _collisionsRoot;
        private Scene _gameScene;
        private DX2D _dx2d;
        private DInput _dInput;
        private RectangleF _clientRect;
        private RenderForm _renderForm;
        private Image _background;
        private GameObject _firstPlayerObject;
        private GameObject _secondPlayerObject;
        private PlayerController _firstPlayerController;
        private PlayerController _secondPlayerController;
        private GameObjectView _firstPlayerView;
        private GameObjectView _secondPlayerView;
        
        public GameSceneCompositeRoot(DX2D dX2D, DInput dInput, RenderForm renderForm, CollisionsCompositeRoot collisionRoot, RectangleF clientRect)
        {
            _clientRect = clientRect;
            _collisionsRoot = collisionRoot;
            _dInput = dInput;
            _dx2d = dX2D;
            _renderForm = renderForm;
            Compose();
        }

        public void Compose()
        {
            int backgroundIndex = _dx2d.LoadBitmap("background.bmp");
            _background = new Image(_dx2d, backgroundIndex, Vector3.Zero, Vector3.Zero);

            int bitmapIndex = _dx2d.LoadBitmap("dash.bmp");

            _firstPlayerObject = new GameObject(new List<Invasion.Core.Interfaces.IComponent>
            {
                new Transform
                {
                    Position = new Vector3(0f, 0f, 0f)
                },
                new SpriteRenderer(_dx2d, bitmapIndex, new Vector3(0.0f, 0f, 0), Vector3.Zero),
            }, Layer.Player);
            _firstPlayerObject.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, _firstPlayerObject, new System.Drawing.Size(1, 1)));

            _secondPlayerObject = new GameObject(new List<Invasion.Core.Interfaces.IComponent>
            {
                new Transform
                {
                    Position = new Vector3(5f, 5f, 0f),
                },
                new SpriteRenderer(_dx2d, bitmapIndex, new Vector3(0, 0, 0), Vector3.Zero),
            }, Layer.Border);
            _secondPlayerObject.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, _secondPlayerObject, new System.Drawing.Size(1, 1)));
            _firstPlayerController = new PlayerController(_firstPlayerObject, new PlayerInput(_dInput, Key.W, Key.S, Key.D, Key.A));
            _secondPlayerController = new PlayerController(_secondPlayerObject, new PlayerInput(_dInput, Key.NumberPad8, Key.NumberPad2, Key.NumberPad6, Key.NumberPad4));

            _firstPlayerView = new GameObjectView(_firstPlayerObject, _clientRect.Height / 25f, _clientRect.Height);
            _secondPlayerView = new GameObjectView(_secondPlayerObject, _clientRect.Height / 25f, _clientRect.Height);
            _gameScene = new Scene(new List<GameObject>
            {
                _firstPlayerObject,
                _secondPlayerObject
            }, new List<Invasion.Core.Interfaces.IController>
            {
                _firstPlayerController,
                _secondPlayerController
            }, new List<Invasion.Core.Interfaces.IView>
            {
                _firstPlayerView,
                _secondPlayerView
            });
        }

        public void Update()
        {
            _background.DrawBackground(1, _clientRect.Height / 25f, 25f/1080, _clientRect.Height);
            _gameScene.Update();
            _gameScene.FixedUpdate();
        }
    }
}
