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

namespace WPFView
{
    public class GameView
    {
        private float _fps = 60;
        public RenderForm RenderForm { get; private set; }
        public IGraphicProvider GraphicsProvider { get; private set; }

        private GameSceneCompositeRoot _game;
        private float _scale;
        private RectangleF _clientRect;
        private IInputProvider _inputProvider;
        WindowRenderTarget renderTarget;

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
            renderTarget = GraphicsProvider.GraphicTarget.Target;
            RenderForm_Resize(this, null);
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
            int width = RenderForm.ClientSize.Width;
            int height = RenderForm.ClientSize.Height;
            renderTarget.Resize(new Size2(width, height));
            _clientRect.Width = renderTarget.Size.Width;
            _clientRect.Height = renderTarget.Size.Height;
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
            RenderForm.Dispose();
        }
    }
}