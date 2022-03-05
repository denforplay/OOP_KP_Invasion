using Invasion.Core.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace Invasion.Engine.Components
{
    public class SpriteRenderer : IComponent
    {
        private static readonly float _pu = 1.0f / 16.0f;

        private DX2D _dx2d;
        private Transform _transform = new Transform();//!!!HARDCODE(VALUE MUST BE INITIALIZED FROM CONSTRUCTOR
        public Transform Transform => _transform;
        private int _bitmapIndex;
        public Core.Size Size => _bitmapSize;
        private Core.Size _bitmapSize;
        private Vector2 _bitmapCenter;
        private Vector2 _translation;
        private Matrix3x2 _matrix;

        public SpriteRenderer(DX2D dx2d, int bitmapIndex, Vector3 position, Vector3 rotation)
        {
            _dx2d = dx2d;
            _bitmapIndex = bitmapIndex;
            Transform.Position = position;
            Transform.Rotation = rotation;
            Transform.Scale = new Vector3(1, 1, 1);
            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[_bitmapIndex];
            _bitmapSize = new Core.Size(bitmap.Size.Width, bitmap.Size.Height);
            _bitmapCenter.X = _bitmapSize.Width / 2.0f;
            _bitmapCenter.Y = _bitmapSize.Height / 2.0f;
        }

        public void Draw(float opacity, float scale, float height)
        {
            _translation.X = (-_bitmapCenter.X * _pu + Transform.Position.X) * scale;
            _translation.Y = height - (-_bitmapCenter.Y * _pu + Transform.Position.Y + 1) * scale;
            _matrix = 
                Matrix3x2.Scaling(scale * _pu, scale * _pu, Vector2.Zero) *
                Matrix3x2.Translation(_translation);

            WindowRenderTarget r = _dx2d.RenderTarget;
            r.Transform = _matrix;

            // Нарисовываемся
            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[_bitmapIndex];
            r.DrawBitmap(bitmap, opacity, BitmapInterpolationMode.Linear);
        }
    }
}
