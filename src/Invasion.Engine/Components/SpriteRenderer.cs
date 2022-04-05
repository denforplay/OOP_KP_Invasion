using Invasion.Engine.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace Invasion.Engine.Components
{
    public class SpriteRenderer : IComponent
    {
        private readonly float _pu = 1.0f / 16.0f;

        private DX2D _dx2d;
        private Transform _transform = new Transform();
        public Transform Transform => _transform;
        public Size Size => _bitmapSize;
        private Size _bitmapSize;
        private Vector2 _bitmapCenter;
        private Vector2 _translation;
        private Matrix3x2 _matrix;
        private string _spriteFileName;
        public SpriteRenderer(DX2D dx2d, string spriteFileName)
        {
            _spriteFileName = spriteFileName;
            _dx2d = dx2d;
            Transform.Scale = new Vector3(1, 1, 1);
            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[spriteFileName];
            _bitmapSize = new Size((int)bitmap.Size.Width, (int)bitmap.Size.Height);
            _bitmapCenter.X = _bitmapSize.Width / 2.0f;
            _bitmapCenter.Y = _bitmapSize.Height / 2.0f;
        }

        public void Draw(float opacity, float scale, float height)
        {
            _translation.X = (-_bitmapCenter.X * _pu + Transform.Position.X) * scale;
            _translation.Y = height - (-_bitmapCenter.Y * _pu + Transform.Position.Y + 1) * scale;
            _matrix =
                Matrix3x2.Rotation(Transform.Rotation.X, _bitmapCenter)*
                Matrix3x2.Scaling(scale * _pu, scale * _pu, Vector2.Zero) *
                Matrix3x2.Translation(_translation) ;

            WindowRenderTarget r = _dx2d.RenderTarget;
            r.Transform = _matrix;

            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[_spriteFileName];
            r.DrawBitmap(bitmap, opacity, BitmapInterpolationMode.Linear);
        }
    }
}
