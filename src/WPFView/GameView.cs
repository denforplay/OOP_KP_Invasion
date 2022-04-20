using Invasion.Engine;
using Invasion.Engine.InputSystem;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invastion.CompositeRoot.Implementations;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Windows;
using System;
using System.Collections.Generic;

namespace WPFView
{
    public class GameView
    {
        private float _fps = 60;
        public RenderForm RenderForm { get; private set; }
        public DirectXGraphicsProvider GraphicsProvider { get; private set; }

        private GameSceneCompositeRoot _game;
        private float _scale;
        private RectangleF _clientRect;
        private IInputProvider _inputProvider;

        public GameView(GameConfiguration gameConfiguration, Dictionary<string, Type> playerWeapons)
        {
            Time.Start(_fps);
            RenderForm = new RenderForm("Direct2D Application");
            RenderForm.ClientSize = new System.Drawing.Size(Screen.Width, Screen.Height);
            RenderForm.TopLevel = false;
            RenderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GraphicsProvider = new DirectXGraphicsProvider(RenderForm);
            _clientRect.Width = RenderForm.ClientSize.Width;
            _clientRect.Height = RenderForm.ClientSize.Height;
            _inputProvider = new DirectXInputProvider(RenderForm);
            _game = new GameSceneCompositeRoot(GraphicsProvider, _inputProvider, playerWeapons, gameConfiguration);
            RenderForm_Resize(this, null);
        }

        private void RenderCallback()
        {
            if (Time.Update())
            {
                _inputProvider.Update();
                WindowRenderTarget target = GraphicsProvider.RenderTarget;
                Size2F targetSize = target.Size;
                _clientRect.Width = targetSize.Width;
                _clientRect.Height = targetSize.Height;
                target.BeginDraw();
                target.Clear(Color.Black);
                _game.Update(_scale);
                target.Transform = Matrix3x2.Identity;
                target.EndDraw();
            }
        }

        private void RenderForm_Resize(object sender, EventArgs e)
        {
            int width = RenderForm.ClientSize.Width;
            int height = RenderForm.ClientSize.Height;
            GraphicsProvider.RenderTarget.Resize(new Size2(width, height));
            _clientRect.Width = GraphicsProvider.RenderTarget.Size.Width;
            _clientRect.Height = GraphicsProvider.RenderTarget.Size.Height;
            _scale = _clientRect.Height / 25f;
        }

        public void Run()
        {
            RenderForm.Resize += RenderForm_Resize;
            RenderLoop.Run(RenderForm, RenderCallback);
        }

        public void Dispose()
        {
            _game.Dispose();
            GraphicsProvider.Dispose();
            RenderForm.Dispose();
        }
    }
}