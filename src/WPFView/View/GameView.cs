using Invasion.Engine;
using Invasion.Engine.Graphics;
using Invasion.Engine.InputSystem;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invastion.CompositeRoot.Implementations;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using WPFView.View;

namespace WPFView
{
    public class GameView : IGameView
    {
        private float _fps = 60;
        private RenderForm _renderForm;
        public RenderForm RenderForm => _renderForm;

        private GameSceneCompositeRoot _game;
        private float _scale;
        private RectangleF _clientRect;
        public IGraphicProvider GraphicsProvider { get; private set; }
        private IInputProvider _inputProvider;
        WindowRenderTarget renderTarget;

        public GameView(GameConfiguration gameConfiguration, Dictionary<string, Type> playerWeapons)
        {
            Time.Start(_fps);
            _renderForm = new RenderForm("Direct2D Application");
            _renderForm.ClientSize = new System.Drawing.Size(Screen.Width, Screen.Height);
            _renderForm.TopLevel = false;
            _renderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GraphicsProvider = new DirectXGraphicsProvider(_renderForm);
            _clientRect.Width = _renderForm.ClientSize.Width;
            _clientRect.Height = _renderForm.ClientSize.Height;
            _inputProvider = new DirectXInputProvider();
            _game = new GameSceneCompositeRoot(GraphicsProvider, _inputProvider, playerWeapons, gameConfiguration);
            renderTarget = GraphicsProvider.GraphicTarget.Target;
            RenderForm_Resize(this, null);
        }

        public void Restart()
        {
            _game.Restart();
        }

        public void Stop()
        {
            _game.StopGame();
        }

        private void RenderCallback()
        {
            if (Time.Update())
            {
                _inputProvider.Update();
                Size2F targetSize = renderTarget.Size;
                _clientRect.Width = targetSize.Width;
                _clientRect.Height = targetSize.Height;
                renderTarget.BeginDraw();
                renderTarget.Clear(Color.Black);
                _game.Update(_scale);
                renderTarget.Transform = Matrix3x2.Identity;
                renderTarget.EndDraw();
            }
        }

        private void RenderForm_Resize(object sender, EventArgs e)
        {
            int width = _renderForm.ClientSize.Width;
            int height = _renderForm.ClientSize.Height;
            renderTarget.Resize(new Size2(width, height));
            _clientRect.Width = renderTarget.Size.Width;
            _clientRect.Height = renderTarget.Size.Height;
            _scale = _clientRect.Height / 25f;
        }

        public void Run()
        {
            _renderForm.Resize += RenderForm_Resize;
            RenderLoop.Run(_renderForm, RenderCallback);
        }

        public void Dispose()
        {
            GraphicsProvider.Dispose();
            _inputProvider.Dispose();
            Utilities.Dispose(ref renderTarget);
            _game.Dispose();
            Utilities.Dispose(ref _renderForm);
        }
    }
}