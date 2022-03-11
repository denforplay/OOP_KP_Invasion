using Invasion.Engine;
using Invasion.Engine.InputSystem;
using Invastion.CompositeRoot.Implementations;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UIApp
{
    public class GameView
    {
        public RenderForm RenderForm { get; private set; }
        public DX2D DX2D { get; private set; }

        private CollisionsCompositeRoot _collisions;
        private GameSceneCompositeRoot _game;
        private float _scale;
        private DInput _dInput;
        private RectangleF _clientRect;

        public GameView(Window window)
        {
            Time.Start();
            _collisions = new CollisionsCompositeRoot();
            RenderForm = new RenderForm("Direct2D Application");
            RenderForm.ClientSize = new System.Drawing.Size(1920, 1080);

            RenderForm.AllowUserResizing = false;
            RenderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            DX2D = new DX2D(RenderForm, window);
            _clientRect.Width = RenderForm.ClientSize.Width;
            _clientRect.Height = RenderForm.ClientSize.Height;
            _dInput = new DInput(RenderForm);
            _game = new GameSceneCompositeRoot(DX2D, _dInput, RenderForm, _clientRect);
            RenderForm_Resize(this, null);
        }

        private void RenderCallback()
        {
            Time.Update();
            _dInput.UpdateKeyboardState();
            _dInput.UpdateMouseState();
            _collisions.Update();
            WindowRenderTarget target = DX2D.RenderTarget;
            Size2F targetSize = target.Size;
            _clientRect.Width = targetSize.Width;
            _clientRect.Height = targetSize.Height;
            target.BeginDraw();
            target.Clear(Color.Black);
            _game.Update();
            target.Transform = Matrix3x2.Identity;
            //target.DrawText($"deltatime:{Time.DeltaTime}\ntime: {Time.FullTime:f1}\nx: {transform.Position.X:f2}, y: {transform.Position.Y:f2}",
            //    DX2D.TextFormatStats, _clientRect, DX2D.WhiteBrush);
            target.EndDraw();
        }

        // При ресайзинге обновляем размер области отображения и масштаб
        private void RenderForm_Resize(object sender, EventArgs e)
        {
            int width = RenderForm.ClientSize.Width;
            int height = RenderForm.ClientSize.Height;
            DX2D.RenderTarget.Resize(new Size2(width, height));
            _clientRect.Width = DX2D.RenderTarget.Size.Width;
            _clientRect.Height = DX2D.RenderTarget.Size.Height;
            _scale = _clientRect.Height / 25f;
        }

        // ПОЕХАЛИ!!! Запуск рабочего цикла приложения (обработчик ресайзинга не забыть повесить на делегат)
        public void Run()
        {
            RenderForm.Resize += RenderForm_Resize;
            RenderLoop.Run(RenderForm, RenderCallback);
        }

        // Убираем за собой, удаляя неуправляемые ресурсы (здесь мамы с веником, чтобы убрала за нами нету, легко спровоцировать утечку памяти)
        public void Dispose()
        {
            _dInput.Dispose();
            DX2D.Dispose();
            RenderForm.Dispose();
        }
    }
}
