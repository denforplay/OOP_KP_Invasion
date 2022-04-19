using Invasion.Engine.Components.Interfaces;
using Invasion.Engine.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace Invasion.Engine.Components
{
    public class SpriteRenderer : IRenderer
    {
        private readonly float _pu = 1.0f / 16.0f;

        private DirectXGraphicsProvider _dx2d;
        private Transform _transform;
        public Size Size => _bitmapSize;
        private Size _bitmapSize;
        private Vector2 _bitmapCenter;
        private Vector2 _translation;
        private Matrix3x2 _matrix;
        private string _spriteFileName;
        public SpriteRenderer(DirectXGraphicsProvider dx2d, string spriteFileName)
        {
            _spriteFileName = spriteFileName;
            _dx2d = dx2d;
            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[spriteFileName];
            _bitmapSize = new Size((int)bitmap.Size.Width, (int)bitmap.Size.Height);
            _bitmapCenter.X = _bitmapSize.Width / 2.0f;
            _bitmapCenter.Y = _bitmapSize.Height / 2.0f;
        }

        public void Draw()
        {
            float height = Screen.Height;
            float scale = Screen.Height / Screen.UnitsPerHeight;
            _translation.X = (-_bitmapCenter.X * _pu + _transform.Position.X) * scale;
            _translation.Y = height - (-_bitmapCenter.Y * _pu + _transform.Position.Y + 1) * scale;
            _matrix =
                Matrix3x2.Rotation(_transform.Rotation.X, _bitmapCenter)*
                Matrix3x2.Scaling(scale * _pu, scale * _pu, Vector2.Zero) *
                Matrix3x2.Translation(_translation) ;

            WindowRenderTarget r = _dx2d.RenderTarget;
            r.Transform = _matrix;

            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[_spriteFileName];
            r.DrawBitmap(bitmap, 1, BitmapInterpolationMode.Linear);
        }

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }
    }
}
