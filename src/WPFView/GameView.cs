using Invasion.Engine;
using Invasion.Engine.InputSystem;
using Invastion.CompositeRoot.Implementations;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Windows;
using System;
using System.Windows;

namespace WPFView
{
    public class GameView
    {

        public RenderForm RenderForm { get; private set; }
        public DX2D DX2D { get; private set; }

        private GameSceneCompositeRoot _game;
        private float _scale;
        private DInput _dInput;
        private RectangleF _clientRect;

        public GameView()
        {
            Time.Start();
            RenderForm = new RenderForm("Direct2D Application");
            RenderForm.ClientSize = new System.Drawing.Size(Screen.Width, Screen.Height);
            RenderForm.TopLevel = false;
            RenderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            DX2D = new DX2D(RenderForm);
            _clientRect.Width = RenderForm.ClientSize.Width;
            _clientRect.Height = RenderForm.ClientSize.Height;
            _dInput = new DInput(RenderForm);
            _game = new GameSceneCompositeRoot(DX2D, _dInput, _clientRect);
            RenderForm_Resize(this, null);
        }

        private void RenderCallback()
        {
            Time.Update();
            _dInput.UpdateKeyboardState();
            WindowRenderTarget target = DX2D.RenderTarget;
            Size2F targetSize = target.Size;
            _clientRect.Width = targetSize.Width;
            _clientRect.Height = targetSize.Height;
            target.BeginDraw();
            target.Clear(Color.Black);
            _game.Update(_scale);
            target.Transform = Matrix3x2.Identity;
            target.EndDraw();
        }

        private void RenderForm_Resize(object sender, EventArgs e)
        {
            int width = RenderForm.ClientSize.Width;
            int height = RenderForm.ClientSize.Height;
            DX2D.RenderTarget.Resize(new Size2(width, height));
            _clientRect.Width = DX2D.RenderTarget.Size.Width;
            _clientRect.Height = DX2D.RenderTarget.Size.Height;
            _scale = _clientRect.Height / 25f;
        }

        public void Run()
        {
            RenderForm.Resize += RenderForm_Resize;
            RenderLoop.Run(RenderForm, RenderCallback);
        }

        public void Dispose()
        {
            _dInput.Dispose();
            DX2D.Dispose();
            RenderForm.Dispose();
        }
    }
}
